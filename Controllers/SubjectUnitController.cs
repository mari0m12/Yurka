using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EduPlatform.Data;
using EduPlatform.Models;
using EduPlatform.Services;
using EduPlatform.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EduPlatform.Controllers
{
    public class SubjectController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IEnrollmentService _enrollment;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubjectController(AppDbContext db, IEnrollmentService enrollment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _enrollment = enrollment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var subject = await _db.Subjects
                .Include(s => s.Units.Where(u => u.IsActive))
                    .ThenInclude(u => u.Lessons)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

            if (subject == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var unitsVm = new List<UnitWithEnrollmentViewModel>();

            foreach (var unit in subject.Units.OrderBy(u => u.SortOrder))
            {
                bool isEnrolled = userId != null && await _enrollment.IsEnrolledAsync(userId, unit.Id);
                bool hasPending = userId != null && await _enrollment.HasPendingPaymentAsync(userId, unit.Id);

                unitsVm.Add(new UnitWithEnrollmentViewModel
                {
                    Unit = unit,
                    IsEnrolled = isEnrolled,
                    HasPendingPayment = hasPending,
                    LessonsCount = unit.Lessons.Count(l => l.IsActive),
                    ExamsCount = await _db.Exams.CountAsync(e => e.UnitId == unit.Id)
                });
            }

            return View(new SubjectDetailViewModel
            {
                Subject = subject,
                Units = unitsVm,
                IsLoggedIn = User.Identity?.IsAuthenticated ?? false
            });
        }
    }

    [Authorize]
    public class UnitController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IEnrollmentService _enrollment;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitController(AppDbContext db, IEnrollmentService enrollment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _enrollment = enrollment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var unit = await _db.Units
                .Include(u => u.Subject)
                .Include(u => u.Lessons.Where(l => l.IsActive))
                    .ThenInclude(l => l.Exams.Where(e => e.IsActive))
                .Include(u => u.Exams.Where(e => e.IsActive && e.ExamType == ExamType.Unit))
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

            if (unit == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            bool isEnrolled = await _enrollment.IsEnrolledAsync(userId, id);

            if (!isEnrolled)
                return RedirectToAction("Purchase", new { id });

            var lessonsVm = new List<LessonWithProgressViewModel>();
            foreach (var lesson in unit.Lessons.OrderBy(l => l.SortOrder))
            {
                var lastAttempt = await _db.ExamAttempts
                    .Where(a => a.UserId == userId && a.Exam.LessonId == lesson.Id && a.FinishedAt != null)
                    .OrderByDescending(a => a.FinishedAt)
                    .FirstOrDefaultAsync();

                lessonsVm.Add(new LessonWithProgressViewModel
                {
                    Lesson = lesson,
                    LessonExams = lesson.Exams.ToList(),
                    LastAttempt = lastAttempt
                });
            }

            return View(new UnitDetailViewModel
            {
                Unit = unit,
                IsEnrolled = true,
                Lessons = lessonsVm,
                UnitExams = unit.Exams.ToList()
            });
        }

        public async Task<IActionResult> Purchase(int id)
        {
            var unit = await _db.Units.Include(u => u.Subject).FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            if (await _enrollment.IsEnrolledAsync(userId, id))
                return RedirectToAction("Detail", new { id });

            var config = new PaymentRequestViewModel { UnitId = id, Unit = unit };
            return View(config);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(PaymentRequestViewModel vm)
        {
            var unit = await _db.Units.Include(u => u.Subject).FirstOrDefaultAsync(u => u.Id == vm.UnitId);
            if (unit == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;

            if (await _enrollment.HasPendingPaymentAsync(userId, vm.UnitId))
            {
                TempData["Warning"] = "لديك طلب دفع قيد المراجعة لهذه الوحدة";
                return RedirectToAction("Detail", "Subject", new { id = unit.SubjectId });
            }

            string? receiptUrl = null;
            if (vm.ReceiptImage != null && vm.ReceiptImage.Length > 0)
            {
                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "receipts");
                Directory.CreateDirectory(uploadsDir);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.ReceiptImage.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await vm.ReceiptImage.CopyToAsync(stream);
                receiptUrl = $"/uploads/receipts/{fileName}";
            }

            var paymentRequest = new PaymentRequest
            {
                UserId = userId,
                UnitId = vm.UnitId,
                Amount = unit.Price,
                VodafonePhone = vm.VodafonePhone,
                TransactionReference = vm.TransactionReference,
                ReceiptImageUrl = receiptUrl,
                Status = PaymentStatus.Pending
            };

            _db.PaymentRequests.Add(paymentRequest);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم إرسال طلب الدفع بنجاح! سيتم مراجعته وتفعيل الوحدة قريباً";
            return RedirectToAction("Dashboard", "Student");
        }

        public async Task<IActionResult> Watch(int id)
        {
            var lesson = await _db.Lessons
                .Include(l => l.Unit)
                .Include(l => l.Exams)
                    .ThenInclude(e => e.Questions)
                        .ThenInclude(q => q.Answers)
                .Include(l => l.VideoQuestions.Where(q => q.IsActive))
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            if (!lesson.IsFreePreview && !await _enrollment.IsEnrolledAsync(userId, lesson.UnitId))
                return RedirectToAction("Purchase", new { id = lesson.UnitId });

            var questionIds = lesson.VideoQuestions.Select(q => q.Id).ToList();
            var responses = await _db.VideoQuestionResponses
                .Include(r => r.SelectedAnswer)
                .Where(r => r.UserId == userId && questionIds.Contains(r.VideoQuestionId))
                .ToListAsync();

            return View(new LessonWatchViewModel
            {
                Lesson = lesson,
                InteractiveQuestions = lesson.VideoQuestions
                    .OrderBy(q => q.TriggerSecond)
                    .ThenBy(q => q.SortOrder)
                    .ToList(),
                Responses = responses,
                AwardedPoints = responses.Sum(r => r.AwardedPoints),
                WrongAnswers = responses.Count(r => !r.IsCorrect)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitVideoQuestion(int questionId, int answerId)
        {
            var question = await _db.VideoQuestions
                .Include(q => q.Lesson)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == questionId && q.IsActive);
            if (question == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            if (!question.Lesson.IsFreePreview && !await _enrollment.IsEnrolledAsync(userId, question.Lesson.UnitId))
                return Forbid();

            var existing = await _db.VideoQuestionResponses
                .FirstOrDefaultAsync(r => r.UserId == userId && r.VideoQuestionId == questionId);
            if (existing != null)
            {
                var currentResponses = await _db.VideoQuestionResponses
                    .Where(r => r.UserId == userId && r.VideoQuestion.LessonId == question.LessonId)
                    .ToListAsync();
                return Json(new
                {
                    alreadyAnswered = true,
                    isCorrect = existing.IsCorrect,
                    awardedPoints = existing.AwardedPoints,
                    totalPoints = currentResponses.Sum(r => r.AwardedPoints),
                    wrongAnswers = currentResponses.Count(r => !r.IsCorrect),
                    maxWrongAnswers = 3
                });
            }

            var selected = question.Answers.FirstOrDefault(a => a.Id == answerId);
            if (selected == null) return BadRequest();

            var wrongBefore = await _db.VideoQuestionResponses
                .CountAsync(r => r.UserId == userId && r.VideoQuestion.LessonId == question.LessonId && !r.IsCorrect);

            var isCorrect = selected.IsCorrect;
            var awarded = isCorrect && wrongBefore < 3 ? question.Points : 0;

            var response = new VideoQuestionResponse
            {
                UserId = userId,
                VideoQuestionId = question.Id,
                SelectedAnswerId = selected.Id,
                IsCorrect = isCorrect,
                AwardedPoints = awarded
            };
            _db.VideoQuestionResponses.Add(response);
            await _db.SaveChangesAsync();

            var lessonResponses = await _db.VideoQuestionResponses
                .Where(r => r.UserId == userId && r.VideoQuestion.LessonId == question.LessonId)
                .ToListAsync();

            return Json(new
            {
                alreadyAnswered = false,
                isCorrect,
                awardedPoints = awarded,
                totalPoints = lessonResponses.Sum(r => r.AwardedPoints),
                wrongAnswers = lessonResponses.Count(r => !r.IsCorrect),
                maxWrongAnswers = 3,
                pointsLocked = wrongBefore >= 3
            });
        }
    }
}

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
    [Authorize]
    public class ExamController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IExamService _examService;
        private readonly IEnrollmentService _enrollment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamController(AppDbContext db, IExamService examService, IEnrollmentService enrollment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _examService = examService;
            _enrollment = enrollment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Start(int id)
        {
            var exam = await _db.Exams
                .Include(e => e.Questions).ThenInclude(q => q.Answers)
                .Include(e => e.Unit).Include(e => e.Lesson)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

            if (exam == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;

            // Check enrollment
            int unitId = exam.UnitId ?? exam.Lesson?.UnitId ?? 0;
            if (!await _enrollment.IsEnrolledAsync(userId, unitId))
                return Forbid();

            // Check max attempts
            if (!await _examService.CanAttemptAsync(userId, id))
            {
                TempData["Error"] = "لقد استنفذت عدد المحاولات المسموح بها لهذا الامتحان";
                return RedirectToAction("Detail", "Unit", new { id = unitId });
            }

            var attempt = await _examService.StartAttemptAsync(userId, id);

            var questions = exam.ShuffleQuestions
                ? exam.Questions.OrderBy(_ => Guid.NewGuid()).ToList()
                : exam.Questions.OrderBy(q => q.SortOrder).ToList();

            var vm = new ExamViewModel
            {
                Exam = exam,
                Questions = questions,
                AttemptId = attempt.Id,
                EndTime = DateTime.UtcNow.AddMinutes(exam.DurationMinutes)
            };

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ExamSubmitViewModel vm)
        {
            var attempt = await _db.ExamAttempts
                .Include(a => a.Exam)
                .FirstOrDefaultAsync(a => a.Id == vm.AttemptId);

            if (attempt == null || attempt.FinishedAt != null)
                return BadRequest();

            var userId = _userManager.GetUserId(User)!;
            if (attempt.UserId != userId) return Forbid();

            var result = await _examService.SubmitAttemptAsync(vm.AttemptId, vm.Answers);
            return RedirectToAction("Result", new { id = result.Id });
        }

        public async Task<IActionResult> Result(int id)
        {
            var attempt = await _db.ExamAttempts
                .Include(a => a.Exam).ThenInclude(e => e.Questions).ThenInclude(q => q.Answers)
                .Include(a => a.AttemptAnswers).ThenInclude(aa => aa.SelectedAnswer)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attempt == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            if (attempt.UserId != userId) return Forbid();

            var questionResults = attempt.Exam.Questions.Select(q =>
            {
                var aa = attempt.AttemptAnswers.FirstOrDefault(x => x.QuestionId == q.Id);
                return new QuestionResultViewModel
                {
                    Question = q,
                    SelectedAnswer = aa?.SelectedAnswer,
                    CorrectAnswer = q.Answers.FirstOrDefault(a => a.IsCorrect),
                    IsCorrect = aa?.IsCorrect ?? false
                };
            }).ToList();

            var vm = new ExamResultViewModel
            {
                Attempt = attempt,
                Exam = attempt.Exam,
                QuestionResults = questionResults,
                ScorePercentage = attempt.TotalPoints > 0
                    ? (attempt.Score * 100 / attempt.TotalPoints)
                    : 0
            };

            return View(vm);
        }
    }

    [Authorize]
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(AppDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = _userManager.GetUserId(User)!;
            var user = await _userManager.FindByIdAsync(userId);

            var enrollments = await _db.Enrollments
                .Include(e => e.Unit).ThenInclude(u => u.Subject)
                .Where(e => e.UserId == userId && e.IsActive)
                .OrderByDescending(e => e.EnrolledAt)
                .ToListAsync();

            var pending = await _db.PaymentRequests
                .Include(p => p.Unit).ThenInclude(u => u.Subject)
                .Where(p => p.UserId == userId && p.Status == PaymentStatus.Pending)
                .ToListAsync();

            var recentAttempts = await _db.ExamAttempts
                .Include(a => a.Exam)
                .Where(a => a.UserId == userId && a.FinishedAt != null)
                .OrderByDescending(a => a.FinishedAt)
                .Take(5)
                .ToListAsync();

            var completedAttempts = await _db.ExamAttempts
                .Where(a => a.UserId == userId && a.FinishedAt != null)
                .ToListAsync();

            var videoResponses = await _db.VideoQuestionResponses
                .Include(r => r.VideoQuestion)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            var totalPoints = completedAttempts.Sum(a => a.Score) + videoResponses.Sum(r => r.AwardedPoints);
            var totalPossible = completedAttempts.Sum(a => a.TotalPoints) + videoResponses.Sum(r => r.VideoQuestion.Points);

            var vm = new StudentDashboardViewModel
            {
                User = user!,
                Enrollments = enrollments,
                PendingPayments = pending,
                RecentAttempts = recentAttempts,
                TotalPoints = totalPoints,
                TotalPossiblePoints = totalPossible,
                AccuracyPercent = totalPossible > 0 ? totalPoints * 100 / totalPossible : 0,
                TotalEnrollments = enrollments.Count,
                PassedExams = completedAttempts.Count(a => a.IsPassed)
            };

            return View(vm);
        }

        public async Task<IActionResult> Notifications()
        {
            var userId = _userManager.GetUserId(User)!;
            var notifications = await _db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            // Mark all as read
            foreach (var n in notifications.Where(n => !n.IsRead))
                n.IsRead = true;
            await _db.SaveChangesAsync();

            return View(notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Avatar()
        {
            var userId = _userManager.GetUserId(User)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Avatar(string profileImage)
        {
            var userId = _userManager.GetUserId(User)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(profileImage) &&
                profileImage.StartsWith("/yurka/assets/", StringComparison.OrdinalIgnoreCase))
            {
                user.ProfileImage = profileImage;
                await _userManager.UpdateAsync(user);
                TempData["Success"] = "تم حفظ صورة البروفايل";
            }

            return RedirectToAction("Dashboard");
        }
    }
}

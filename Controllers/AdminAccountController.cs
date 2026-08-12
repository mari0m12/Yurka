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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEnrollmentService _enrollmentService;
        private readonly INotificationService _notificationService;

        public AdminController(AppDbContext db, UserManager<ApplicationUser> userManager,
            IEnrollmentService enrollmentService, INotificationService notificationService)
        {
            _db = db;
            _userManager = userManager;
            _enrollmentService = enrollmentService;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new AdminDashboardViewModel
            {
                TotalStudents = await _db.Users.CountAsync(),
                TotalSubjects = await _db.Subjects.CountAsync(),
                TotalUnits = await _db.Units.CountAsync(),
                TotalLessons = await _db.Lessons.CountAsync(),
                PendingPayments = await _db.PaymentRequests.CountAsync(p => p.Status == PaymentStatus.Pending),
                TotalRevenue = (int)await _db.PaymentRequests.Where(p => p.Status == PaymentStatus.Confirmed).SumAsync(p => p.Amount),
                RecentPayments = await _db.PaymentRequests
                    .Include(p => p.User).Include(p => p.Unit).ThenInclude(u => u.Subject)
                    .OrderByDescending(p => p.RequestedAt).Take(10).ToListAsync(),
                RecentStudents = await _db.Users.OrderByDescending(u => u.CreatedAt).Take(10).ToListAsync()
            };
            return View(vm);
        }

        public async Task<IActionResult> Payments(PaymentStatus? status)
        {
            var query = _db.PaymentRequests
                .Include(p => p.User).Include(p => p.Unit).ThenInclude(u => u.Subject)
                .AsQueryable();

            if (status.HasValue) query = query.Where(p => p.Status == status.Value);

            var payments = await query.OrderByDescending(p => p.RequestedAt).ToListAsync();
            return View(new AdminPaymentsViewModel { Payments = payments, FilterStatus = status });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPayment(int id, string? adminNotes)
        {
            var payment = await _db.PaymentRequests.Include(p => p.Unit).FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null) return NotFound();

            payment.Status = PaymentStatus.Confirmed;
            payment.AdminNotes = adminNotes;
            payment.ProcessedAt = DateTime.UtcNow;

            var existingEnrollment = await _enrollmentService.GetEnrollmentAsync(payment.UserId, payment.UnitId);
            if (existingEnrollment == null)
                await _enrollmentService.CreateEnrollmentAsync(payment.UserId, payment.UnitId, payment.Id);

            await _db.SaveChangesAsync();

            await _notificationService.SendAsync(
                payment.UserId,
                "تم تفعيل الوحدة",
                $"تم قبول طلب الدفع وتفعيل الوحدة: {payment.Unit.Title}",
                $"/Unit/Detail/{payment.UnitId}"
            );

            TempData["Success"] = "تم تأكيد الدفع وتفعيل الوحدة للطالب";
            return RedirectToAction("Payments");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectPayment(int id, string? adminNotes)
        {
            var payment = await _db.PaymentRequests.FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null) return NotFound();

            payment.Status = PaymentStatus.Rejected;
            payment.AdminNotes = adminNotes;
            payment.ProcessedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            await _notificationService.SendAsync(
                payment.UserId,
                "تم رفض طلب الدفع",
                $"تم رفض طلب الدفع. السبب: {adminNotes ?? "يرجى التواصل مع الإدارة"}",
                "/Student/Dashboard"
            );

            TempData["Warning"] = "تم رفض طلب الدفع";
            return RedirectToAction("Payments");
        }

        public async Task<IActionResult> Subjects()
        {
            var subjects = await _db.Subjects
                .Include(s => s.Units)
                .OrderBy(s => s.GradeLevel).ThenBy(s => s.SortOrder)
                .ToListAsync();
            return View(subjects);
        }

        [HttpGet]
        public IActionResult CreateSubject() => View(new Subject());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject(Subject model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Subjects.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم إضافة المادة بنجاح";
            return RedirectToAction("Subjects");
        }

        [HttpGet]
        public async Task<IActionResult> EditSubject(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(Subject model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Subjects.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم تحديث المادة";
            return RedirectToAction("Subjects");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _db.Subjects
                .Include(s => s.Units).ThenInclude(u => u.Lessons).ThenInclude(l => l.Exams)
                .Include(s => s.Units).ThenInclude(u => u.Exams)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();

            var examIds = subject.Units
                .SelectMany(u => u.Exams.Concat(u.Lessons.SelectMany(l => l.Exams)))
                .Select(e => e.Id)
                .Distinct()
                .ToList();

            await RemoveExamAttemptsAsync(examIds);
            _db.Exams.RemoveRange(subject.Units.SelectMany(u => u.Exams.Concat(u.Lessons.SelectMany(l => l.Exams))));
            _db.Units.RemoveRange(subject.Units);
            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم حذف المادة وكل الوحدات والامتحانات التابعة لها";
            return RedirectToAction("Subjects");
        }

        public async Task<IActionResult> Units(int id)
        {
            var subject = await _db.Subjects
                .Include(s => s.Units)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUnit(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return View(new UnitFormViewModel { SubjectId = id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUnit(UnitFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var thumbUrl = await SaveThumbnailAsync(vm.Thumbnail);

            _db.Units.Add(new Unit
            {
                Title = vm.Title,
                Description = vm.Description,
                Price = vm.Price,
                SortOrder = vm.SortOrder,
                SubjectId = vm.SubjectId,
                ThumbnailUrl = thumbUrl
            });
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم إضافة الوحدة بنجاح";
            return RedirectToAction("Units", new { id = vm.SubjectId });
        }

        [HttpGet]
        public async Task<IActionResult> EditUnit(int id)
        {
            var unit = await _db.Units.FindAsync(id);
            if (unit == null) return NotFound();
            return View("CreateUnit", new UnitFormViewModel
            {
                Id = unit.Id,
                Title = unit.Title,
                Description = unit.Description,
                Price = unit.Price,
                SortOrder = unit.SortOrder,
                SubjectId = unit.SubjectId,
                ExistingThumbnail = unit.ThumbnailUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUnit(UnitFormViewModel vm)
        {
            var unit = await _db.Units.FindAsync(vm.Id);
            if (unit == null) return NotFound();
            if (!ModelState.IsValid) return View("CreateUnit", vm);

            unit.Title = vm.Title;
            unit.Description = vm.Description;
            unit.Price = vm.Price;
            unit.SortOrder = vm.SortOrder;
            unit.ThumbnailUrl = await SaveThumbnailAsync(vm.Thumbnail) ?? unit.ThumbnailUrl;

            await _db.SaveChangesAsync();
            TempData["Success"] = "تم تحديث الوحدة";
            return RedirectToAction("Units", new { id = unit.SubjectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _db.Units
                .Include(u => u.Lessons).ThenInclude(l => l.Exams)
                .Include(u => u.Exams)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound();
            var subjectId = unit.SubjectId;
            var exams = unit.Exams.Concat(unit.Lessons.SelectMany(l => l.Exams)).ToList();
            var examIds = exams.Select(e => e.Id).Distinct().ToList();

            await RemoveExamAttemptsAsync(examIds);
            _db.Exams.RemoveRange(exams);
            _db.Units.Remove(unit);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم حذف الوحدة وكل محتواها";
            return RedirectToAction("Units", new { id = subjectId });
        }

        public async Task<IActionResult> Lessons(int id)
        {
            var unit = await _db.Units.Include(u => u.Lessons).Include(u => u.Subject).FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound();
            return View(unit);
        }

        [HttpGet]
        public IActionResult CreateLesson(int id) => View(new LessonFormViewModel { UnitId = id });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLesson(LessonFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            _db.Lessons.Add(new Lesson
            {
                Title = vm.Title,
                Description = vm.Description,
                YoutubeVideoId = vm.YoutubeVideoId,
                DurationMinutes = vm.DurationMinutes,
                SortOrder = vm.SortOrder,
                IsFreePreview = vm.IsFreePreview,
                UnitId = vm.UnitId
            });
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم إضافة الدرس بنجاح";
            return RedirectToAction("Lessons", new { id = vm.UnitId });
        }

        public async Task<IActionResult> VideoQuestions(int lessonId)
        {
            var lesson = await _db.Lessons
                .Include(l => l.Unit)
                .Include(l => l.VideoQuestions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(l => l.Id == lessonId);
            if (lesson == null) return NotFound();

            return View(new VideoQuestionAdminViewModel
            {
                Lesson = lesson,
                Questions = lesson.VideoQuestions
                    .OrderBy(q => q.TriggerSecond)
                    .ThenBy(q => q.SortOrder)
                    .ToList()
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVideoQuestion(int lessonId, string text, int triggerMinute, int triggerSecond, int points, List<string> answerTexts, int correctAnswerIndex)
        {
            var lesson = await _db.Lessons.FindAsync(lessonId);
            if (lesson == null) return NotFound();

            var cleanAnswers = answerTexts.Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a.Trim()).ToList();
            if (string.IsNullOrWhiteSpace(text) || cleanAnswers.Count < 2 || correctAnswerIndex < 0 || correctAnswerIndex >= cleanAnswers.Count)
            {
                TempData["Error"] = "اكتب السؤال وإجابتين على الأقل وحدد الإجابة الصح";
                return RedirectToAction("VideoQuestions", new { lessonId });
            }

            var trigger = Math.Max(0, (triggerMinute * 60) + Math.Clamp(triggerSecond, 0, 59));
            var question = new VideoQuestion
            {
                LessonId = lessonId,
                Text = text.Trim(),
                TriggerSecond = trigger,
                Points = Math.Clamp(points, 1, 10),
                SortOrder = await _db.VideoQuestions.CountAsync(q => q.LessonId == lessonId)
            };
            _db.VideoQuestions.Add(question);
            await _db.SaveChangesAsync();

            for (var i = 0; i < cleanAnswers.Count; i++)
            {
                _db.VideoQuestionAnswers.Add(new VideoQuestionAnswer
                {
                    VideoQuestionId = question.Id,
                    Text = cleanAnswers[i],
                    IsCorrect = i == correctAnswerIndex,
                    SortOrder = i
                });
            }

            await _db.SaveChangesAsync();
            TempData["Success"] = "تم إضافة سؤال الفيديو";
            return RedirectToAction("VideoQuestions", new { lessonId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleVideoQuestion(int id)
        {
            var question = await _db.VideoQuestions.FindAsync(id);
            if (question == null) return NotFound();
            question.IsActive = !question.IsActive;
            await _db.SaveChangesAsync();
            return RedirectToAction("VideoQuestions", new { lessonId = question.LessonId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVideoQuestion(int id)
        {
            var question = await _db.VideoQuestions.FindAsync(id);
            if (question == null) return NotFound();
            var lessonId = question.LessonId;
            _db.VideoQuestions.Remove(question);
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم حذف سؤال الفيديو";
            return RedirectToAction("VideoQuestions", new { lessonId });
        }

        public async Task<IActionResult> Exams(int? unitId, int? lessonId)
        {
            var query = _db.Exams.Include(e => e.Unit).Include(e => e.Lesson).Include(e => e.Questions).AsQueryable();
            if (unitId.HasValue) query = query.Where(e => e.UnitId == unitId);
            if (lessonId.HasValue) query = query.Where(e => e.LessonId == lessonId);
            return View(await query.OrderByDescending(e => e.CreatedAt).ToListAsync());
        }

        [HttpGet]
        public IActionResult CreateExam(int? unitId, int? lessonId)
            => View(new ExamFormViewModel
            {
                UnitId = unitId,
                LessonId = lessonId,
                ExamType = lessonId.HasValue ? ExamType.Lesson : ExamType.Unit
            });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExam(ExamFormViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var exam = new Exam
            {
                Title = model.Title,
                ExamType = model.ExamType,
                DurationMinutes = model.DurationMinutes,
                PassingScore = model.PassingScore,
                MaxAttempts = model.MaxAttempts,
                IsActive = model.IsActive,
                ShuffleQuestions = model.ShuffleQuestions,
                UnitId = model.UnitId,
                LessonId = model.LessonId
            };
            _db.Exams.Add(exam);
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageQuestions", new { examId = exam.Id });
        }

        [HttpGet]
        public async Task<IActionResult> EditExam(int id)
        {
            var exam = await _db.Exams.FindAsync(id);
            if (exam == null) return NotFound();
            return View("CreateExam", new ExamFormViewModel
            {
                Id = exam.Id,
                Title = exam.Title,
                ExamType = exam.ExamType,
                DurationMinutes = exam.DurationMinutes,
                PassingScore = exam.PassingScore,
                MaxAttempts = exam.MaxAttempts,
                IsActive = exam.IsActive,
                ShuffleQuestions = exam.ShuffleQuestions,
                UnitId = exam.UnitId,
                LessonId = exam.LessonId
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExam(ExamFormViewModel model)
        {
            var exam = await _db.Exams.FindAsync(model.Id);
            if (exam == null) return NotFound();
            if (!ModelState.IsValid) return View("CreateExam", model);

            exam.Title = model.Title;
            exam.ExamType = model.ExamType;
            exam.DurationMinutes = model.DurationMinutes;
            exam.PassingScore = model.PassingScore;
            exam.MaxAttempts = model.MaxAttempts;
            exam.IsActive = model.IsActive;
            exam.ShuffleQuestions = model.ShuffleQuestions;
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم تحديث الامتحان";
            return RedirectToAction("Exams", new { unitId = exam.UnitId, lessonId = exam.LessonId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _db.Exams.FindAsync(id);
            if (exam == null) return NotFound();
            var unitId = exam.UnitId;
            var lessonId = exam.LessonId;

            await RemoveExamAttemptsAsync(new[] { id });
            _db.Exams.Remove(exam);
            await _db.SaveChangesAsync();

            TempData["Success"] = "تم حذف الامتحان";
            return RedirectToAction("Exams", new { unitId, lessonId });
        }

        public async Task<IActionResult> ManageQuestions(int examId)
        {
            var exam = await _db.Exams.Include(e => e.Questions).ThenInclude(q => q.Answers).FirstOrDefaultAsync(e => e.Id == examId);
            if (exam == null) return NotFound();
            return View(exam);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(int examId, string text, int points, List<string> answerTexts, int correctAnswerIndex)
        {
            var question = new Question { ExamId = examId, Text = text, Points = points };
            _db.Questions.Add(question);
            await _db.SaveChangesAsync();

            for (var i = 0; i < answerTexts.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(answerTexts[i]))
                {
                    _db.Answers.Add(new Answer
                    {
                        QuestionId = question.Id,
                        Text = answerTexts[i],
                        IsCorrect = i == correctAnswerIndex
                    });
                }
            }
            await _db.SaveChangesAsync();
            TempData["Success"] = "تم إضافة السؤال";
            return RedirectToAction("ManageQuestions", new { examId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int id, int examId)
        {
            var q = await _db.Questions.FindAsync(id);
            if (q != null)
            {
                _db.Questions.Remove(q);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("ManageQuestions", new { examId });
        }

        public async Task<IActionResult> Students()
        {
            var students = await _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult CreateStudent()
            => View("StudentForm", new AdminStudentFormViewModel { ProfileImage = "/yurka/assets/avatars/avatar-01.png" });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(AdminStudentFormViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Password))
                ModelState.AddModelError(nameof(vm.Password), "كلمة المرور مطلوبة عند إضافة طالب جديد");
            if (!ModelState.IsValid) return View("StudentForm", vm);

            var user = new ApplicationUser
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.Email,
                Phone = vm.Phone,
                ParentPhone = vm.ParentPhone,
                Governorate = vm.Governorate,
                Markaz = vm.Markaz,
                GradeLevel = vm.GradeLevel,
                IsActive = vm.IsActive,
                ProfileImage = string.IsNullOrWhiteSpace(vm.ProfileImage) ? "/yurka/assets/avatars/avatar-01.png" : vm.ProfileImage
            };

            var result = await _userManager.CreateAsync(user, vm.Password!);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
                TempData["Success"] = "تم إضافة الطالب";
                return RedirectToAction("Students");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View("StudentForm", vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View("StudentForm", new AdminStudentFormViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? "",
                Phone = user.Phone,
                ParentPhone = user.ParentPhone,
                Governorate = user.Governorate,
                Markaz = user.Markaz,
                ProfileImage = user.ProfileImage,
                GradeLevel = user.GradeLevel,
                IsActive = user.IsActive
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(AdminStudentFormViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id ?? "");
            if (user == null) return NotFound();
            if (!ModelState.IsValid) return View("StudentForm", vm);

            user.FullName = vm.FullName;
            user.Email = vm.Email;
            user.UserName = vm.Email;
            user.Phone = vm.Phone;
            user.ParentPhone = vm.ParentPhone;
            user.Governorate = vm.Governorate;
            user.Markaz = vm.Markaz;
            user.ProfileImage = vm.ProfileImage;
            user.GradeLevel = vm.GradeLevel;
            user.IsActive = vm.IsActive;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors) ModelState.AddModelError("", error.Description);
                return View("StudentForm", vm);
            }

            if (!string.IsNullOrWhiteSpace(vm.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, vm.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors) ModelState.AddModelError("", error.Description);
                    return View("StudentForm", vm);
                }
            }

            TempData["Success"] = "تم تحديث بيانات الطالب";
            return RedirectToAction("Students");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            _db.ExamAttempts.RemoveRange(await _db.ExamAttempts.Where(a => a.UserId == id).ToListAsync());
            _db.VideoQuestionResponses.RemoveRange(await _db.VideoQuestionResponses.Where(a => a.UserId == id).ToListAsync());
            _db.Enrollments.RemoveRange(await _db.Enrollments.Where(e => e.UserId == id).ToListAsync());
            _db.PaymentRequests.RemoveRange(await _db.PaymentRequests.Where(p => p.UserId == id).ToListAsync());
            _db.Notifications.RemoveRange(await _db.Notifications.Where(n => n.UserId == id).ToListAsync());
            await _db.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Error"] = "تعذر حذف الطالب";
                return RedirectToAction("Students");
            }

            TempData["Success"] = "تم حذف الطالب";
            return RedirectToAction("Students");
        }

        public async Task<IActionResult> StudentDetail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            ViewBag.Enrollments = await _db.Enrollments.Include(e => e.Unit).ThenInclude(u => u.Subject).Where(e => e.UserId == id).ToListAsync();
            ViewBag.Attempts = await _db.ExamAttempts.Include(a => a.Exam).Where(a => a.UserId == id && a.FinishedAt != null).OrderByDescending(a => a.FinishedAt).ToListAsync();
            return View(user);
        }

        private async Task<string?> SaveThumbnailAsync(IFormFile? thumbnail)
        {
            if (thumbnail == null || thumbnail.Length == 0) return null;
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "thumbnails");
            Directory.CreateDirectory(dir);
            var file = $"{Guid.NewGuid()}{Path.GetExtension(thumbnail.FileName)}";
            using var stream = new FileStream(Path.Combine(dir, file), FileMode.Create);
            await thumbnail.CopyToAsync(stream);
            return $"/uploads/thumbnails/{file}";
        }

        private async Task RemoveExamAttemptsAsync(IEnumerable<int> examIds)
        {
            var ids = examIds.Distinct().ToList();
            if (!ids.Any()) return;
            var attempts = await _db.ExamAttempts.Where(a => ids.Contains(a.ExamId)).ToListAsync();
            _db.ExamAttempts.RemoveRange(attempts);
        }
    }

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(vm);

            var loginId = vm.Email.Trim();
            var user = loginId.Contains('@')
                ? await _userManager.FindByEmailAsync(loginId)
                : await _userManager.Users.FirstOrDefaultAsync(u => u.Phone == loginId || u.FullName == loginId);

            var result = user == null
                ? Microsoft.AspNetCore.Identity.SignInResult.Failed
                : await _signInManager.PasswordSignInAsync(user.UserName!, vm.Password, vm.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "بيانات الدخول غير صحيحة");
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // الإيميل بيتولد من التليفون في الـ hidden field
            // لو جه فاضي (مثلاً JavaScript مش شغال) نولده هنا
            if (string.IsNullOrWhiteSpace(vm.Email))
            {
                var cleaned = (vm.Phone ?? "").Trim().Replace(" ", "");
                vm.Email = cleaned + "@yurka.edu";
            }

            // تأكد إن الإيميل مش مكرر (نفس التليفون مش مسجل قبل كده)
            var existingByPhone = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Phone == vm.Phone);
            if (existingByPhone != null)
            {
                ModelState.AddModelError("Phone", "رقم الهاتف ده مسجل قبل كده، حاول تسجل دخول");
                return View(vm);
            }

            // لو الإيميل المولد متكرر (نادر بس نأمن) نضيف رقم
            var existingByEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (existingByEmail != null)
            {
                vm.Email = vm.Phone!.Trim() + "_" + DateTime.Now.Ticks + "@yurka.edu";
            }

            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.Email,
                Phone = vm.Phone,
                ParentPhone = vm.ParentPhone,
                Governorate = vm.Governorate,
                Markaz = vm.Markaz,
                ProfileImage = string.IsNullOrWhiteSpace(vm.ProfileImage) ? "/yurka/assets/avatars/avatar-01.png" : vm.ProfileImage,
                GradeLevel = vm.GradeLevel
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Dashboard", "Student");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}

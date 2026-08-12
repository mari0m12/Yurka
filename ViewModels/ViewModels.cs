using System.ComponentModel.DataAnnotations;
using EduPlatform.Models;

namespace EduPlatform.ViewModels
{
    // ===== AUTH =====
    public class LoginViewModel
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم ولي الأمر مطلوب")]
        public string ParentPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "المحافظة مطلوبة")]
        public string Governorate { get; set; } = string.Empty;

        [Required(ErrorMessage = "المركز مطلوب")]
        public string Markaz { get; set; } = string.Empty;

        public string? ProfileImage { get; set; }

        [Required]
        public GradeLevel GradeLevel { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [Compare("Password", ErrorMessage = "كلمتا المرور غير متطابقتين")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    // ===== HOME =====
    public class HomeViewModel
    {
        public List<GradeSubjectsViewModel> GradeSubjects { get; set; } = new();
        public int TotalStudents { get; set; }
        public int TotalLessons { get; set; }
        public int TotalSubjects { get; set; }
        public List<LeaderboardStudentViewModel> Leaderboard { get; set; } = new();
    }

    public class LeaderboardStudentViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string? Governorate { get; set; }
        public string? Markaz { get; set; }
        public int Points { get; set; }
    }

    public class GradeSubjectsViewModel
    {
        public GradeLevel Grade { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public List<Subject> Subjects { get; set; } = new();
    }

    // ===== SUBJECT / UNIT =====
    public class SubjectDetailViewModel
    {
        public Subject Subject { get; set; } = null!;
        public List<UnitWithEnrollmentViewModel> Units { get; set; } = new();
        public bool IsLoggedIn { get; set; }
    }

    public class UnitWithEnrollmentViewModel
    {
        public Unit Unit { get; set; } = null!;
        public bool IsEnrolled { get; set; }
        public bool HasPendingPayment { get; set; }
        public int LessonsCount { get; set; }
        public int ExamsCount { get; set; }
    }

    public class UnitDetailViewModel
    {
        public Unit Unit { get; set; } = null!;
        public bool IsEnrolled { get; set; }
        public List<LessonWithProgressViewModel> Lessons { get; set; } = new();
        public List<Exam> UnitExams { get; set; } = new();
        public Enrollment? Enrollment { get; set; }
    }

    public class LessonWithProgressViewModel
    {
        public Lesson Lesson { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public List<Exam> LessonExams { get; set; } = new();
        public ExamAttempt? LastAttempt { get; set; }
    }

    public class LessonWatchViewModel
    {
        public Lesson Lesson { get; set; } = null!;
        public List<VideoQuestion> InteractiveQuestions { get; set; } = new();
        public List<VideoQuestionResponse> Responses { get; set; } = new();
        public int AwardedPoints { get; set; }
        public int WrongAnswers { get; set; }
        public int MaxWrongAnswers { get; set; } = 3;
    }

    // ===== PAYMENT =====
    public class PaymentRequestViewModel
    {
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        [Required(ErrorMessage = "رقم فودافون كاش مطلوب")]
        public string VodafonePhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "رقم المرجع مطلوب")]
        public string TransactionReference { get; set; } = string.Empty;

        public IFormFile? ReceiptImage { get; set; }
    }

    // ===== EXAM =====
    public class ExamViewModel
    {
        public Exam Exam { get; set; } = null!;
        public List<Question> Questions { get; set; } = new();
        public int AttemptId { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ExamSubmitViewModel
    {
        public int AttemptId { get; set; }
        public int ExamId { get; set; }
        public Dictionary<int, int> Answers { get; set; } = new(); // QuestionId -> AnswerId
    }

    public class ExamResultViewModel
    {
        public ExamAttempt Attempt { get; set; } = null!;
        public Exam Exam { get; set; } = null!;
        public List<QuestionResultViewModel> QuestionResults { get; set; } = new();
        public int ScorePercentage { get; set; }
    }

    public class QuestionResultViewModel
    {
        public Question Question { get; set; } = null!;
        public Answer? SelectedAnswer { get; set; }
        public Answer? CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }

    // ===== STUDENT DASHBOARD =====
    public class StudentDashboardViewModel
    {
        public ApplicationUser User { get; set; } = null!;
        public List<Enrollment> Enrollments { get; set; } = new();
        public List<PaymentRequest> PendingPayments { get; set; } = new();
        public List<ExamAttempt> RecentAttempts { get; set; } = new();
        public int TotalPoints { get; set; }
        public int TotalPossiblePoints { get; set; }
        public int AccuracyPercent { get; set; }
        public int TotalEnrollments { get; set; }
        public int PassedExams { get; set; }
    }

    // ===== ADMIN =====
    public class AdminDashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalSubjects { get; set; }
        public int TotalUnits { get; set; }
        public int TotalLessons { get; set; }
        public int PendingPayments { get; set; }
        public int TotalRevenue { get; set; }
        public List<PaymentRequest> RecentPayments { get; set; } = new();
        public List<ApplicationUser> RecentStudents { get; set; } = new();
    }

    public class AdminPaymentsViewModel
    {
        public List<PaymentRequest> Payments { get; set; } = new();
        public PaymentStatus? FilterStatus { get; set; }
    }

    public class AdminStudentFormViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? ParentPhone { get; set; }
        public string? Governorate { get; set; }
        public string? Markaz { get; set; }
        public string? ProfileImage { get; set; }
        public GradeLevel? GradeLevel { get; set; }
        public bool IsActive { get; set; } = true;

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

    public class ExamFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الامتحان مطلوب")]
        public string Title { get; set; } = string.Empty;

        public ExamType ExamType { get; set; } = ExamType.Unit;
        public int DurationMinutes { get; set; } = 30;
        public int PassingScore { get; set; } = 60;
        public int MaxAttempts { get; set; } = 3;
        public bool IsActive { get; set; } = true;
        public bool ShuffleQuestions { get; set; } = true;
        public int? UnitId { get; set; }
        public int? LessonId { get; set; }
    }

    // ===== UNIT CREATE/EDIT =====
    public class UnitFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الوحدة مطلوب")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        public int SortOrder { get; set; }
        public int SubjectId { get; set; }
        public IFormFile? Thumbnail { get; set; }
        public string? ExistingThumbnail { get; set; }
    }

    // ===== LESSON CREATE/EDIT =====
    public class LessonFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الدرس مطلوب")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "رابط يوتيوب مطلوب")]
        public string YoutubeVideoId { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }
        public int SortOrder { get; set; }
        public bool IsFreePreview { get; set; }
        public int UnitId { get; set; }
    }

    public class VideoQuestionAdminViewModel
    {
        public Lesson Lesson { get; set; } = null!;
        public List<VideoQuestion> Questions { get; set; } = new();
    }
}

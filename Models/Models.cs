using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EduPlatform.Models
{
    // ===== ENUMS =====
    public enum GradeLevel
    {
        Primary1 = 1, Primary2 = 2, Primary3 = 3,
        Primary4 = 4, Primary5 = 5, Primary6 = 6,
        Middle1 = 7, Middle2 = 8, Middle3 = 9
    }

    public enum PaymentStatus
    {
        Pending,
        Confirmed,
        Rejected
    }

    public enum ExamType
    {
        Lesson,
        Unit
    }

    // ===== USER =====
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? ParentPhone { get; set; }
        public string? Governorate { get; set; }
        public string? Markaz { get; set; }
        public string? ProfileImage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public GradeLevel? GradeLevel { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();
        public ICollection<VideoQuestionResponse> VideoQuestionResponses { get; set; } = new List<VideoQuestionResponse>();
        public ICollection<PaymentRequest> PaymentRequests { get; set; } = new List<PaymentRequest>();
    }

    // ===== SUBJECT =====
    public class Subject
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public string? IconClass { get; set; }
        public string? Color { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }

    // ===== UNIT =====
    public class Unit
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ThumbnailUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

    // ===== LESSON =====
    public class Lesson
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        public string YoutubeVideoId { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFreePreview { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<VideoQuestion> VideoQuestions { get; set; } = new List<VideoQuestion>();
    }

    // ===== VIDEO INTERACTIVE QUESTIONS =====
    public class VideoQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public int TriggerSecond { get; set; }
        public int Points { get; set; } = 1;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        public ICollection<VideoQuestionAnswer> Answers { get; set; } = new List<VideoQuestionAnswer>();
        public ICollection<VideoQuestionResponse> Responses { get; set; } = new List<VideoQuestionResponse>();
    }

    public class VideoQuestionAnswer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
        public int SortOrder { get; set; }

        public int VideoQuestionId { get; set; }
        public VideoQuestion VideoQuestion { get; set; } = null!;
    }

    public class VideoQuestionResponse
    {
        public int Id { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.Now;
        public bool IsCorrect { get; set; }
        public int AwardedPoints { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int VideoQuestionId { get; set; }
        public VideoQuestion VideoQuestion { get; set; } = null!;

        public int? SelectedAnswerId { get; set; }
        public VideoQuestionAnswer? SelectedAnswer { get; set; }
    }

    // ===== EXAM =====
    public class Exam
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public ExamType ExamType { get; set; }
        public int DurationMinutes { get; set; } = 30;
        public int PassingScore { get; set; } = 60;
        public int MaxAttempts { get; set; } = 3;
        public bool IsActive { get; set; } = true;
        public bool ShuffleQuestions { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? UnitId { get; set; }
        public Unit? Unit { get; set; }

        public int? LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<ExamAttempt> Attempts { get; set; } = new List<ExamAttempt>();
    }

    // ===== QUESTION =====
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
        public int Points { get; set; } = 1;
        public int SortOrder { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }

    // ===== ANSWER =====
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }

    // ===== EXAM ATTEMPT =====
    public class ExamAttempt
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime? FinishedAt { get; set; }
        public int Score { get; set; }
        public int TotalPoints { get; set; }
        public bool IsPassed { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();
    }

    // ===== ATTEMPT ANSWER =====
    public class AttemptAnswer
    {
        public int Id { get; set; }

        public int AttemptId { get; set; }
        public ExamAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int? SelectedAnswerId { get; set; }
        public Answer? SelectedAnswer { get; set; }

        public bool IsCorrect { get; set; }
    }

    // ===== ENROLLMENT =====
    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public int? PaymentRequestId { get; set; }
        public PaymentRequest? PaymentRequest { get; set; }
    }

    // ===== PAYMENT REQUEST =====
    public class PaymentRequest
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public string? VodafonePhone { get; set; }
        public string? TransactionReference { get; set; }
        public string? ReceiptImageUrl { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? AdminNotes { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public DateTime? ProcessedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public Enrollment? Enrollment { get; set; }
    }

    // ===== NOTIFICATION =====
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Link { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}

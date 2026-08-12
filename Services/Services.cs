using Microsoft.EntityFrameworkCore;
using EduPlatform.Data;
using EduPlatform.Models;

namespace EduPlatform.Services
{
    public interface IEnrollmentService
    {
        Task<bool> IsEnrolledAsync(string userId, int unitId);
        Task<bool> HasPendingPaymentAsync(string userId, int unitId);
        Task<Enrollment?> GetEnrollmentAsync(string userId, int unitId);
        Task<Enrollment> CreateEnrollmentAsync(string userId, int unitId, int paymentRequestId);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _db;
        public EnrollmentService(AppDbContext db) => _db = db;

        public async Task<bool> IsEnrolledAsync(string userId, int unitId)
            => await _db.Enrollments.AnyAsync(e => e.UserId == userId && e.UnitId == unitId && e.IsActive);

        public async Task<bool> HasPendingPaymentAsync(string userId, int unitId)
            => await _db.PaymentRequests.AnyAsync(p =>
                p.UserId == userId && p.UnitId == unitId && p.Status == PaymentStatus.Pending);

        public async Task<Enrollment?> GetEnrollmentAsync(string userId, int unitId)
            => await _db.Enrollments.FirstOrDefaultAsync(e => e.UserId == userId && e.UnitId == unitId);

        public async Task<Enrollment> CreateEnrollmentAsync(string userId, int unitId, int paymentRequestId)
        {
            var enrollment = new Enrollment
            {
                UserId = userId,
                UnitId = unitId,
                PaymentRequestId = paymentRequestId,
                IsActive = true
            };
            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();
            return enrollment;
        }
    }

    public interface IExamService
    {
        Task<ExamAttempt> StartAttemptAsync(string userId, int examId);
        Task<ExamAttempt> SubmitAttemptAsync(int attemptId, Dictionary<int, int> answers);
        Task<int> GetAttemptCountAsync(string userId, int examId);
        Task<bool> CanAttemptAsync(string userId, int examId);
    }

    public class ExamService : IExamService
    {
        private readonly AppDbContext _db;
        public ExamService(AppDbContext db) => _db = db;

        public async Task<int> GetAttemptCountAsync(string userId, int examId)
            => await _db.ExamAttempts.CountAsync(a => a.UserId == userId && a.ExamId == examId && a.FinishedAt != null);

        public async Task<bool> CanAttemptAsync(string userId, int examId)
        {
            var exam = await _db.Exams.FindAsync(examId);
            if (exam == null) return false;
            var count = await GetAttemptCountAsync(userId, examId);
            return count < exam.MaxAttempts;
        }

        public async Task<ExamAttempt> StartAttemptAsync(string userId, int examId)
        {
            var attempt = new ExamAttempt { UserId = userId, ExamId = examId };
            _db.ExamAttempts.Add(attempt);
            await _db.SaveChangesAsync();
            return attempt;
        }

        public async Task<ExamAttempt> SubmitAttemptAsync(int attemptId, Dictionary<int, int> answers)
        {
            var attempt = await _db.ExamAttempts
                .Include(a => a.Exam).ThenInclude(e => e.Questions).ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(a => a.Id == attemptId)
                ?? throw new InvalidOperationException("Attempt not found");

            int score = 0;
            int total = 0;

            foreach (var question in attempt.Exam.Questions)
            {
                total += question.Points;
                var attemptAnswer = new AttemptAnswer
                {
                    AttemptId = attemptId,
                    QuestionId = question.Id
                };

                if (answers.TryGetValue(question.Id, out int selectedAnswerId))
                {
                    var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == selectedAnswerId);
                    if (selectedAnswer != null)
                    {
                        attemptAnswer.SelectedAnswerId = selectedAnswerId;
                        attemptAnswer.IsCorrect = selectedAnswer.IsCorrect;
                        if (selectedAnswer.IsCorrect) score += question.Points;
                    }
                }

                _db.AttemptAnswers.Add(attemptAnswer);
            }

            attempt.Score = score;
            attempt.TotalPoints = total;
            attempt.IsPassed = total > 0 && (score * 100 / total) >= attempt.Exam.PassingScore;
            attempt.FinishedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return attempt;
        }
    }

    public interface INotificationService
    {
        Task SendAsync(string userId, string title, string message, string? link = null);
    }

    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _db;
        public NotificationService(AppDbContext db) => _db = db;

        public async Task SendAsync(string userId, string title, string message, string? link = null)
        {
            _db.Notifications.Add(new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Link = link
            });
            await _db.SaveChangesAsync();
        }
    }
}

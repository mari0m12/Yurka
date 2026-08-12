using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EduPlatform.Models;

namespace EduPlatform.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<ExamAttempt> ExamAttempts => Set<ExamAttempt>();
        public DbSet<AttemptAnswer> AttemptAnswers => Set<AttemptAnswer>();
        public DbSet<VideoQuestion> VideoQuestions => Set<VideoQuestion>();
        public DbSet<VideoQuestionAnswer> VideoQuestionAnswers => Set<VideoQuestionAnswer>();
        public DbSet<VideoQuestionResponse> VideoQuestionResponses => Set<VideoQuestionResponse>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<PaymentRequest> PaymentRequests => Set<PaymentRequest>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Subject
            builder.Entity<Subject>()
                .HasIndex(s => new { s.GradeLevel, s.SortOrder });

            // Unit
            builder.Entity<Unit>()
                .HasOne(u => u.Subject)
                .WithMany(s => s.Units)
                .HasForeignKey(u => u.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lesson
            builder.Entity<Lesson>()
                .HasOne(l => l.Unit)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Video interactive questions
            builder.Entity<VideoQuestion>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.VideoQuestions)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<VideoQuestion>()
                .HasIndex(q => new { q.LessonId, q.TriggerSecond });

            builder.Entity<VideoQuestionAnswer>()
                .HasOne(a => a.VideoQuestion)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.VideoQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<VideoQuestionResponse>()
                .HasOne(r => r.User)
                .WithMany(u => u.VideoQuestionResponses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VideoQuestionResponse>()
                .HasOne(r => r.VideoQuestion)
                .WithMany(q => q.Responses)
                .HasForeignKey(r => r.VideoQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<VideoQuestionResponse>()
                .HasOne(r => r.SelectedAnswer)
                .WithMany()
                .HasForeignKey(r => r.SelectedAnswerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<VideoQuestionResponse>()
                .HasIndex(r => new { r.UserId, r.VideoQuestionId })
                .IsUnique();

            // Exam -> Unit (optional)
            builder.Entity<Exam>()
                .HasOne(e => e.Unit)
                .WithMany(u => u.Exams)
                .HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Exam -> Lesson (optional)
            builder.Entity<Exam>()
                .HasOne(e => e.Lesson)
                .WithMany(l => l.Exams)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrollment - unique constraint
            builder.Entity<Enrollment>()
                .HasIndex(e => new { e.UserId, e.UnitId })
                .IsUnique();

            // PaymentRequest
            builder.Entity<PaymentRequest>()
                .HasOne(p => p.User)
                .WithMany(u => u.PaymentRequests)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ExamAttempt
            builder.Entity<ExamAttempt>()
                .HasOne(ea => ea.User)
                .WithMany(u => u.ExamAttempts)
                .HasForeignKey(ea => ea.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AttemptAnswer - منع cascade paths المتعددة
            builder.Entity<AttemptAnswer>()
                .HasOne(aa => aa.Attempt)
                .WithMany(a => a.AttemptAnswers)
                .HasForeignKey(aa => aa.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AttemptAnswer>()
                .HasOne(aa => aa.Question)
                .WithMany()
                .HasForeignKey(aa => aa.QuestionId)
                .OnDelete(DeleteBehavior.NoAction); // ← ده الحل

            builder.Entity<AttemptAnswer>()
                .HasOne(aa => aa.SelectedAnswer)
                .WithMany()
                .HasForeignKey(aa => aa.SelectedAnswerId)
                .OnDelete(DeleteBehavior.NoAction); // ← وده

            // Seed Admin Role
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().HasData(
                new Microsoft.AspNetCore.Identity.IdentityRole
                {
                    Id = "admin-role-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Microsoft.AspNetCore.Identity.IdentityRole
                {
                    Id = "student-role-id",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            );
        }
    }
}

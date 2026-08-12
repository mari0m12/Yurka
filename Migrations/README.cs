// =====================================================================
// MIGRATION INSTRUCTIONS
// =====================================================================
// Run these commands in the Package Manager Console or terminal:
//
//   dotnet ef migrations add InitialCreate --project EduPlatform
//   dotnet ef database update --project EduPlatform
//
// OR in Package Manager Console (Visual Studio):
//   Add-Migration InitialCreate
//   Update-Database
//
// =====================================================================
// The migration will auto-generate from your DbContext.
// After running, the database will contain all tables:
//
//   - AspNetUsers          (طلاب + أدمن)
//   - AspNetRoles          (Admin, Student)
//   - AspNetUserRoles
//   - Subjects             (المواد الدراسية)
//   - Units                (الوحدات)
//   - Lessons              (الدروس)
//   - Exams                (الامتحانات)
//   - Questions            (الأسئلة)
//   - Answers              (الإجابات)
//   - ExamAttempts         (محاولات الامتحانات)
//   - AttemptAnswers       (إجابات الطالب)
//   - Enrollments          (الاشتراكات)
//   - PaymentRequests      (طلبات الدفع)
//   - Notifications        (الإشعارات)
//
// Default Admin Account (seeded automatically on first run):
//   Email:    admin@eduplatform.com
//   Password: Admin@123456
// =====================================================================

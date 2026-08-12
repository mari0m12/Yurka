using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduPlatform.Data;
using EduPlatform.Models;
using EduPlatform.ViewModels;

namespace EduPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var subjects = await _db.Subjects
                .Where(s => s.IsActive)
                .Include(s => s.Units.Where(u => u.IsActive))
                .OrderBy(s => s.GradeLevel)
                .ThenBy(s => s.SortOrder)
                .ToListAsync();

            var gradeGroups = subjects
                .GroupBy(s => s.GradeLevel)
                .Select(g => new GradeSubjectsViewModel
                {
                    Grade = g.Key,
                    GradeName = GetGradeName(g.Key),
                    Subjects = g.ToList()
                })
                .ToList();

            var adminRoleId = await _db.Roles
                .Where(r => r.NormalizedName == "ADMIN")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            var adminUserIds = string.IsNullOrEmpty(adminRoleId)
                ? new List<string>()
                : await _db.UserRoles
                    .Where(ur => ur.RoleId == adminRoleId)
                    .Select(ur => ur.UserId)
                    .ToListAsync();

            var leaderboard = await _db.Users
                .Where(u => !adminUserIds.Contains(u.Id))
                .Select(u => new LeaderboardStudentViewModel
                {
                    Name = u.FullName,
                    ProfileImage = u.ProfileImage,
                    Governorate = u.Governorate,
                    Markaz = u.Markaz,
                    Points = u.ExamAttempts.Where(a => a.FinishedAt != null).Sum(a => a.Score)
                        + u.VideoQuestionResponses.Sum(r => r.AwardedPoints)
                })
                .OrderByDescending(x => x.Points)
                .Take(8)
                .ToListAsync();

            var vm = new HomeViewModel
            {
                GradeSubjects = gradeGroups,
                TotalStudents = await _db.Users.CountAsync(u => !adminUserIds.Contains(u.Id)),
                TotalLessons = await _db.Lessons.CountAsync(),
                TotalSubjects = await _db.Subjects.CountAsync(),
                Leaderboard = leaderboard
            };

            return View(vm);
        }

        private static string GetGradeName(GradeLevel grade) => grade switch
        {
            GradeLevel.Primary1 => "الصف الأول الابتدائي",
            GradeLevel.Primary2 => "الصف الثاني الابتدائي",
            GradeLevel.Primary3 => "الصف الثالث الابتدائي",
            GradeLevel.Primary4 => "الصف الرابع الابتدائي",
            GradeLevel.Primary5 => "الصف الخامس الابتدائي",
            GradeLevel.Primary6 => "الصف السادس الابتدائي",
            GradeLevel.Middle1 => "الصف الأول الإعدادي",
            GradeLevel.Middle2 => "الصف الثاني الإعدادي",
            GradeLevel.Middle3 => "الصف الثالث الإعدادي",
            _ => "غير محدد"
        };

        public IActionResult Privacy() => View();
        public IActionResult Error() => View();
    }
}

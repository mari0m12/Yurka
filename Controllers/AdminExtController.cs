using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EduPlatform.Data;
using EduPlatform.Models;
using EduPlatform.ViewModels;

namespace EduPlatform.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminExtController : Controller
    {
        private readonly AppDbContext _db;
        public AdminExtController(AppDbContext db) => _db = db;

        [HttpGet, Route("Admin/EditLesson/{id}")]
        public async Task<IActionResult> EditLesson(int id)
        {
            var lesson = await _db.Lessons.FindAsync(id);
            if (lesson == null) return NotFound();

            return View("~/Views/Admin/EditLesson.cshtml", new LessonFormViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                YoutubeVideoId = lesson.YoutubeVideoId,
                DurationMinutes = lesson.DurationMinutes,
                SortOrder = lesson.SortOrder,
                IsFreePreview = lesson.IsFreePreview,
                UnitId = lesson.UnitId
            });
        }

        [HttpPost, Route("Admin/EditLesson/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLessonPost(int id, LessonFormViewModel vm)
        {
            var lesson = await _db.Lessons.FindAsync(id);
            if (lesson == null) return NotFound();
            if (!ModelState.IsValid) return View("~/Views/Admin/EditLesson.cshtml", vm);

            lesson.Title = vm.Title;
            lesson.Description = vm.Description;
            lesson.YoutubeVideoId = vm.YoutubeVideoId;
            lesson.DurationMinutes = vm.DurationMinutes;
            lesson.SortOrder = vm.SortOrder;
            lesson.IsFreePreview = vm.IsFreePreview;

            await _db.SaveChangesAsync();
            TempData["Success"] = "تم تحديث الدرس بنجاح";
            return RedirectToAction("Lessons", "Admin", new { id = lesson.UnitId });
        }

        [HttpPost, Route("Admin/ToggleUnit/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUnit(int id)
        {
            var unit = await _db.Units.FindAsync(id);
            if (unit == null) return NotFound();

            unit.IsActive = !unit.IsActive;
            await _db.SaveChangesAsync();
            TempData["Success"] = unit.IsActive ? "تم تفعيل الوحدة" : "تم إخفاء الوحدة";
            return RedirectToAction("Units", "Admin", new { id = unit.SubjectId });
        }

        [HttpPost, Route("Admin/ToggleExam/{id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleExam(int id)
        {
            var exam = await _db.Exams.FindAsync(id);
            if (exam == null) return NotFound();

            exam.IsActive = !exam.IsActive;
            await _db.SaveChangesAsync();
            TempData["Success"] = exam.IsActive ? "تم تفعيل الامتحان" : "تم إخفاء الامتحان";
            return RedirectToAction("Exams", "Admin", new { unitId = exam.UnitId, lessonId = exam.LessonId });
        }
    }
}

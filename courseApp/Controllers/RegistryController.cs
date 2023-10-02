using courseApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace courseApp.Controllers
{
    public class RegistryController : Controller
    {
        private readonly DataContext _context;
        public RegistryController(DataContext context)
        {
            _context = context;
        } 

        public async Task<IActionResult> Index()
        {
            var CourseRegistry = await _context
                                    .Registries
                                    .Include(x => x.Student)
                                    .Include(x => x.Course) 
                                    .ToListAsync();
            return View(CourseRegistry);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Students = new SelectList(await _context.Students.ToListAsync(),"StudentId","FullName") ;
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(),"CourseId","Title") ;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registry model)
        {
            int studentId = model.StudentId;
            int courseId = model.CourseId;

            bool isAlreadyRegistered = await _context
                                                .Registries
                                                .AnyAsync(r => r.StudentId == studentId && r.CourseId == courseId);

             if (isAlreadyRegistered)
            {
                ModelState.AddModelError("", "Bu öğrenci zaten bu kursa kayıtlı.");
                ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "FullName");
                ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "Title");
                return View(model);
            }

            model.RegisterTime = DateTime.Now;
            _context.Registries.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
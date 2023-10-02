using courseApp.Data;
using courseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace courseApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;
        public CourseController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Courses = await _context
                                    .Courses
                                    .Include(x => x.Teacher) 
                                    .ToListAsync();
            return View(Courses);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(new Course() {CourseId = model.CourseId,Title = model.Title, TeacherId = model.TeacherId});
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "FullName");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var student = await _context.Students.FindAsync(id);
            var course = await _context
                                    .Courses
                                    .Include(s => s.Registries)
                                    .ThenInclude(s => s.Student)
                                    .Select(k => new CourseViewModel 
                                    {
                                        CourseId = k.CourseId,
                                        Title = k.Title,
                                        TeacherId = k.TeacherId,
                                        Registries = k.Registries
                                    })
                                    .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "FullName");
            return View(course);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CourseViewModel model)
        {
            if (id != model.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Course() {CourseId = model.CourseId,Title = model.Title, TeacherId = model.TeacherId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Courses.Any(c => c.CourseId == model.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
                
            }
            ViewBag.Teachers = new SelectList(await _context.Teachers.ToListAsync(), "TeacherId", "FullName");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
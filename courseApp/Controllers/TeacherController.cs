using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using courseApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace courseApp.Controllers
{
    public class TeacherController : Controller
    {
         private readonly DataContext _context;
        public TeacherController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers.ToListAsync());
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher model)
        {
            _context.Teachers.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var student = await _context.Students.FindAsync(id);
            var teacher = await _context
                                    .Teachers
                                    .Include(s => s.Courses)
                                    .FirstOrDefaultAsync(s => s.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Teacher model)
        {
            if (id != model.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Teachers.Any(s => s.TeacherId == model.TeacherId))
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
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }
            
            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day9Todo.Models;
using Task = Day9Todo.Models.Task;

namespace Day9Todo.Controllers
{
    public class TasksController : Controller
    {
        private readonly Datacontext _context;

        public TasksController(Datacontext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var OrderList = _context.Tasks
                .OrderBy(x=>x.IsComplete) 
                .OrderByDescending(x => x.Priority)
                .OrderBy(x=>x.Deadline)
                .ToList();
            var ol = (from task in _context.Tasks
                      orderby task.Deadline
                      orderby task.Priority descending
                      orderby task.IsComplete
                      select task).ToList();
            return _context.Tasks != null ? 
                //View(await _context.Tasks.ToListAsync()) :
                View(ol):
                Problem("Entity set 'Datacontext.Tasks'  is null.");
        }

        public IActionResult OnlyTodo()
        {
            var TaskTodo = _context.Tasks
                .Where(x => !x.IsComplete)
                .OrderByDescending(x => x.Priority)
                .OrderBy(x => x.Deadline)
                .ToList();
            return View("Views/Tasks/Index.cshtml",TaskTodo);
        }

        public IActionResult OnlyIsComplete()
        {
            var TaskComplete = _context.Tasks
                .Where (x => x.IsComplete)
                .OrderByDescending(x => x.Priority)
                .OrderBy(x => x.Deadline)
                .ToList();
            return View("Views/Tasks/Index.cshtml", TaskComplete);
        }

        public IActionResult Today()
        {
            var Today = _context.Tasks
                .Where(x => x.Deadline.Date==DateTime.Now.Date)
                .OrderBy(x => x.IsComplete)
                .OrderByDescending(x => x.Priority)
                .OrderBy(x => x.Deadline)
                .ToList();
            return View("Views/Tasks/Index.cshtml", Today);
        }


        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View(new Task()
            {
                Title = string.Empty,
                Description = string.Empty,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Deadline = DateTime.Now,
                IsComplete = false,
                Priority = Task.ItemPriority.Low
            });
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Created,Updated,Deadline,IsComplete,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,Updated,Deadline,IsComplete,Priority")] Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.Updated = DateTime.Now;
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'Datacontext.Tasks'  is null.");
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
          return (_context.Tasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

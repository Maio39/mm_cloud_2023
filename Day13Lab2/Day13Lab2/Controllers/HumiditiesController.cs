using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day13Lab2.Models;

namespace Day13Lab2.Controllers
{
    public class HumiditiesController : Controller
    {
        private readonly DataContext _context;

        public HumiditiesController(DataContext context)
        {
            _context = context;
        }

        // GET: Humidities
        public async Task<IActionResult> Index()
        {
              return _context.Humidity != null ? 
                          View(await _context.Humidity.ToListAsync()) :
                          Problem("Entity set 'DataContext.Humidity'  is null.");
        }

        // GET: Humidities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Humidity == null)
            {
                return NotFound();
            }

            var humidity = await _context.Humidity
                .FirstOrDefaultAsync(m => m.HumidityID == id);
            if (humidity == null)
            {
                return NotFound();
            }

            return View(humidity);
        }

        // GET: Humidities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Humidities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HumidityID,Value,Unit")] Humidity humidity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(humidity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(humidity);
        }

        // GET: Humidities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Humidity == null)
            {
                return NotFound();
            }

            var humidity = await _context.Humidity.FindAsync(id);
            if (humidity == null)
            {
                return NotFound();
            }
            return View(humidity);
        }

        // POST: Humidities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HumidityID,Value,Unit")] Humidity humidity)
        {
            if (id != humidity.HumidityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(humidity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HumidityExists(humidity.HumidityID))
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
            return View(humidity);
        }

        // GET: Humidities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Humidity == null)
            {
                return NotFound();
            }

            var humidity = await _context.Humidity
                .FirstOrDefaultAsync(m => m.HumidityID == id);
            if (humidity == null)
            {
                return NotFound();
            }

            return View(humidity);
        }

        // POST: Humidities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Humidity == null)
            {
                return Problem("Entity set 'DataContext.Humidity'  is null.");
            }
            var humidity = await _context.Humidity.FindAsync(id);
            if (humidity != null)
            {
                _context.Humidity.Remove(humidity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HumidityExists(int id)
        {
          return (_context.Humidity?.Any(e => e.HumidityID == id)).GetValueOrDefault();
        }
    }
}

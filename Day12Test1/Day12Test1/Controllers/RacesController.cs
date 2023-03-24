using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day12Test1.Models;
using System.Diagnostics;

namespace Day12Test1.Controllers
{
    public class RacesController : Controller
    {
        private readonly DataContext _context;

        public RacesController(DataContext context)
        {
            _context = context;
        }

        // GET: Races
        public async Task<IActionResult> Index()
        {
              return _context.Races != null ? 
                          View(await _context.Races
                          .Include(m=>m.Racer)
                          .ToListAsync()) :
                          Problem("Entity set 'DataContext.Races'  is null.");
        }

        // GET: Races/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // GET: Races/Create
        public IActionResult Create()
        {
            //2
            Taxi t = _context.Taxi
                            .Include(m => m.Races)
                            .Where(m => m.IsActive)
                            .OrderBy(m => m.Races.Count())
                            .First();
            ViewData["Racer"] = t;
            //*/
            return View();
        }

        // POST: Races/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TaxiId,[Bind("RaceId,RaceName,RaceDestination,KmRace,RaceDate")] Race race)
        {
            if (ModelState.IsValid)
            {
                /*/1
                Taxi t = _context.Taxi
                            .Include(m => m.Races)
                            .Where(m=>m.IsActive)
                            .OrderBy(m => m.Races.Count())
                            .First();
                
                race.Racer = t;
                //*/
                Taxi taxi = _context.Taxi.Find(TaxiId);
                race.Racer = taxi;
                _context.Add(race);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(race);
        }

        // GET: Races/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races.FindAsync(id);
            if (race == null)
            {
                return NotFound();
            }
            return View(race);
        }

        // POST: Races/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaceId,RaceName,RaceDestination,KmRace,RaceDate")] Race race)
        {
            if (id != race.RaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(race);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaceExists(race.RaceId))
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
            return View(race);
        }

        // GET: Races/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Races == null)
            {
                return NotFound();
            }

            var race = await _context.Races
                .FirstOrDefaultAsync(m => m.RaceId == id);
            if (race == null)
            {
                return NotFound();
            }

            return View(race);
        }

        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Races == null)
            {
                return Problem("Entity set 'DataContext.Races'  is null.");
            }
            var race = await _context.Races.FindAsync(id);
            if (race != null)
            {
                _context.Races.Remove(race);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaceExists(int id)
        {
          return (_context.Races?.Any(e => e.RaceId == id)).GetValueOrDefault();
        }
    }
}

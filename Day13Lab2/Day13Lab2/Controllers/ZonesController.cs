using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day13Lab2.Models;
using System.Security.Policy;
using Zone = Day13Lab2.Models.Zone;

namespace Day13Lab2.Controllers
{
    public class ZonesController : Controller
    {
        private readonly DataContext _context;

        public ZonesController(DataContext context)
        {
            _context = context;
            /*/
            _context.Humidity.RemoveRange(_context.Humidity);
            _context.Zones.RemoveRange(_context.Zones);
            _context.Temperatures.RemoveRange(_context.Temperatures);
            _context.SaveChanges();
            //*/
            //Popola il DB
            if (_context.Humidity.Count() == 0)
            {
                for (int t = 0; t < 20; t++)
                {
                    Humidity Hum = new Humidity()
                    {
                        Value = new Random().Next(50, 100),
                        Unit = "%"
                    };
                    for (int i = 0; i < new Random().Next(10, 25); i++)
                    {
                        var zon = new Zone()
                        {
                            Description = $"Zone {i}{t}",
                            IsActive = true,
                            TargetTemperature = 21
                        };
                        int indexTemp = new Random().Next(2, 15);
                        for (int j = 0; j < indexTemp; j++)
                        {
                            zon.Rilevazioni.Add(new Temperature()
                            {
                                TemperatureDate = DateTime.Now,
                                TemperatureValue = new Random().Next(14, 30)
                            });
                        }
                        Hum.Zones.Add(zon);
                    }
                    _context.Humidity.Add(Hum);

                }
                _context.SaveChanges();
            }

        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Zones
                .Include(z=>z.Rilevazioni)
                .Include(z => z.Humidity);
            return View(await dataContext.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zones == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones
                .Include(z=>z.Rilevazioni)
                .Include(z => z.Humidity)
                .FirstOrDefaultAsync(m => m.ZoneID == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            ViewData["HumidityID"] = new SelectList(_context.Humidity, "HumidityID", "Value");
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneID,Description,IsActive,TargetTemperature,HumidityID")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                zone.Humidity = _context.Humidity.Find(zone.HumidityID);
                _context.Add(zone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HumidityID"] = new SelectList(_context.Humidity, "HumidityID", "Value", zone.HumidityID);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zones == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones.FindAsync(id);
            if (zone == null)
            {
                return NotFound();
            }
            ViewData["HumidityID"] = new SelectList(_context.Humidity, "HumidityID", "HumidityID", zone.HumidityID);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZoneID,Description,IsActive,TargetTemperature,HumidityID")] Zone zone)
        {
            if (id != zone.ZoneID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    zone.Humidity = _context.Humidity.Find(zone.HumidityID);
                    _context.Update(zone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneExists(zone.ZoneID))
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
            ViewData["HumidityID"] = new SelectList(_context.Humidity, "HumidityID", "HumidityID", zone.HumidityID);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zones == null)
            {
                return NotFound();
            }

            var zone = await _context.Zones
                .Include(z => z.Humidity)
                .FirstOrDefaultAsync(m => m.ZoneID == id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zones == null)
            {
                return Problem("Entity set 'DataContext.Zones'  is null.");
            }
            var zone = await _context.Zones.FindAsync(id);
            if (zone != null)
            {
                _context.Zones.Remove(zone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(int id)
        {
          return (_context.Zones?.Any(e => e.ZoneID == id)).GetValueOrDefault();
        }
    }
}

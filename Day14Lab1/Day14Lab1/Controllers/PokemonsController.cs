using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day14Lab1.Models;

namespace Day14Lab1.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly DataContext _context;

        public PokemonsController(DataContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
              return _context.Pokemons != null ? 
                          View(await _context.Pokemons.ToListAsync()) :
                          Problem("Entity set 'DataContext.Pokemons'  is null.");
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(m => m.PokemonID == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        //Add picture to pokemon
        public IActionResult AddPicture(int id = 0)
        {
            ViewData["PokemonID"] = id;
            return View();

        }
        [HttpPost]
        public IActionResult AddPictureData(int id)
        {
            Pokemon p = _context.Pokemons.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                var fileName = file.FileName;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    Picture picture = new Picture()
                    {
                        PictureName = fileName,
                        RawData = ms.ToArray()
                    };

                    _context.Pictures.Add(picture);
                    if (p.Picture != null)
                    {
                        _context.Pictures.Remove(p.Picture);
                    }
                    p.Picture = picture;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PokemonID,PokemonName,PokemonWeight,PokemonLevel,PokemonXP,PokemonAttack,PokemonDefense,PokemonSpecialAttack,PokemonSpecialDefense,PokemonSpeed,PokemonLifePoints,PokemonStatus,IsMale,IsFemale,IsLegendary,IsEgg")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PokemonID,PokemonName,PokemonWeight,PokemonLevel,PokemonXP,PokemonAttack,PokemonDefense,PokemonSpecialAttack,PokemonSpecialDefense,PokemonSpeed,PokemonLifePoints,PokemonStatus,IsMale,IsFemale,IsLegendary,IsEgg")] Pokemon pokemon)
        {
            if (id != pokemon.PokemonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.PokemonID))
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
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pokemons == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(m => m.PokemonID == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pokemons == null)
            {
                return Problem("Entity set 'DataContext.Pokemons'  is null.");
            }
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
          return (_context.Pokemons?.Any(e => e.PokemonID == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day14Lab1.Models;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace Day14Lab1.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly DataContext _context;

        public PokemonsController(DataContext context)
        {
            _context = context;
            //PopolateDB
            if (_context.Pokemons.Count() == 0)
            {
                var json = JsonObject.Parse(System.IO.File.ReadAllText("pokedex.json"));
                var htp = new HttpClient();
                foreach (var item in json.AsArray())
                {
                    Pokemon poke = new Pokemon()
                    {
                        PokemonName = item!["name"]["english"].ToString(),
                        PokemonWeight = new Random().Next(1, 100),
                        PokemonLevel = new Random().Next(1, 100),
                        PokemonXP = new Random().Next(1, 100), 
                        PokemonStatus = 1,
                        IsLegendary = false,
                        IsMale = false,
                        IsFemale = false,
                        IsEgg = false
                    };
                    if (item!["base"] != null)
                    {
                        poke.PokemonAttack = (int)item!["base"]["Attack"];
                        poke.PokemonDefense = (int)item!["base"]["Defense"];
                        poke.PokemonSpecialAttack = (int)item!["base"]["Sp. Attack"];
                        poke.PokemonSpecialDefense = (int)item!["base"]["Sp. Defense"];
                        poke.PokemonSpeed = (int)item!["base"]["Speed"];
                        poke.PokemonLifePoints = (int)item!["base"]["HP"];
                    }
                    if (item["image"]["hires"] != null)
                    {
                        var res = htp.GetAsync(item["image"]["hires"].AsValue().ToString()).Result;
                        if (res.IsSuccessStatusCode)
                        {
                            byte[] img = res.Content.ReadAsByteArrayAsync().Result;
                            Picture pic = new Picture()
                            {
                                PictureName = poke.PokemonName,
                                RawData = img
                            };
                            _context.Pictures.Add(pic);
                            poke.Picture = pic;
                        }
                    }
                    else
                    {
                        var res = htp.GetAsync("https://www.brasscraft.com/wp-content/uploads/2017/01/no-image-available.png").Result;
                        if (res.IsSuccessStatusCode)
                        {
                            byte[] img = res.Content.ReadAsByteArrayAsync().Result;
                            Picture pic = new Picture()
                            {
                                PictureName = poke.PokemonName,
                                RawData = img
                            };
                            _context.Pictures.Add(pic);
                            poke.Picture = pic;
                        }
                    }
                    _context.Pokemons.Add(poke);
                }
                _context.SaveChanges();
            }
        }

        // GET: Pokemons
        public async Task<IActionResult> Index(int page = 1, int size = 50)
        {
            if (page < 1)
                page = 1;
            ViewData["Page"] = page;
            ViewData["Size"] = size;
            ViewData["TotPages"] = _context.Pokemons.Count() / size;
            return _context.Pokemons != null ? 
                          View(await _context.Pokemons
                          .Include(x=>x.Picture)
                          .Skip(size*(page-1))
                          .Take(size)
                          .ToListAsync()) :
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
                .Include(x=>x.Picture)
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

        public IActionResult GetPicture(int Id)
        {
            Picture picture = _context.Pictures.Find(Id);
            if (picture == null)
            {
                return NotFound();
            }
            var rawData = picture.RawData;

            MemoryStream ms1 = new MemoryStream(rawData);
            /*/Watermark
            Bitmap bitmap = new Bitmap(Bitmap.FromStream(ms1));

            Graphics graphic = Graphics.FromImage(bitmap);

            string myWater = $"MMLogo {DateTime.Now}";
            Font font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            Size l_w = graphic.MeasureString(myWater, font).ToSize();
            Point p = new Point(10, 20);
            Point p1 = new Point(11, 21);

            //Background
            graphic.FillRectangle(new SolidBrush(Color.Aquamarine), new Rectangle(p, l_w));
            //Subpixeling
            graphic.DrawString(myWater, font, Brushes.Black, p1);
            //
            graphic.DrawString(myWater, font, Brushes.White, p);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return File(ms.ToArray(), "image/png");
            }
            //*/
            return File(ms1.ToArray(), "image/png");
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
                        if (pokemon.Picture != null)
                        {
                            _context.Pictures.Remove(pokemon.Picture);
                        }
                        pokemon.Picture = picture;
                    }
                }
                else
                {
                    Picture picture = _context.Pictures.Find(6);
                    pokemon.Picture = picture;
                }
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

            var pokemon = await _context.Pokemons
                .FindAsync(id);
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
                .Include(x=>x.Picture)
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

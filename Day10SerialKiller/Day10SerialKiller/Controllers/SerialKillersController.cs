using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day10SerialKiller;
using Day10SerialKillerb;

namespace Day10SerialKiller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialKillersController : ControllerBase
    {
        private readonly DataContext _context;

        public SerialKillersController(DataContext context)
        {
            _context = context;
            /*/
            if(context.SerialKillers.Count() == 0)
            {
                for(int i=0;i<50;i++)
                {
                    SerialKiller rk = new SerialKiller();
                    SerialKiller RandomKiller = rk.GetRandomKiller();
                    if(RandomKiller != null)
                    {
                        context.SerialKillers.Add(RandomKiller);
                    }
                }
                context.SaveChanges();
            }
            //*/
        }

        [HttpGet("PopolateDb")]
        public IActionResult PopolateDB()
        {
            string path = "middle-names.txt";
            string path2 = "names.txt";
            if (System.IO.File.Exists(path) && System.IO.File.Exists(path2))
            {
                _context.SerialKillers.RemoveRange(_context.SerialKillers);
                _context.SaveChanges();

                List<string> surname = new List<string>(System.IO.File.ReadAllLines(path));
                List<string> name = new List<string>(System.IO.File.ReadAllLines(path2));
                for(int i = 0; i < name.Count; i++)
                {
                    int indexSurname = new Random().Next(0, surname.Count);
                    int indexName = new Random().Next(0, name.Count);  
                    SerialKiller newKiller = new SerialKiller()
                    {
                        Name = name[indexName],
                        Surname = surname[indexSurname],
                        Description = $"Sono il killer {name[indexName]} {surname[indexSurname]}",
                        Killing = new Random().Next(0, 20),
                        IsInJail = new Random().NextInt64() % 2 == 0
                    };
                    _context.SerialKillers.Add(newKiller);
                }
                _context.SaveChanges();
                return Ok(_context.SerialKillers
                                .Take(10)
                                .ToList()
                );
                
            }
            else
            {
                return null;
                throw new ArgumentException("Files Doesn't exists");
            }
        }

        [HttpGet("GetFirst50Killer")]
        public ActionResult<List<SerialKiller>> GetFirst50Killer()
        {
            var killerlist =  _context.SerialKillers
                .Take(50)
                .ToList();
            return Ok(killerlist);
        }

        // GET: api/SerialKillers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerialKiller>>> GetSerialKillers()
        {
          if (_context.SerialKillers == null)
          {
              return NotFound();
          }
            return await _context.SerialKillers.ToListAsync();
        }

        // GET: api/SerialKillers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SerialKiller>> GetSerialKiller(int id)
        {
          if (_context.SerialKillers == null)
          {
              return NotFound();
          }
            var serialKiller = await _context.SerialKillers.FindAsync(id);

            if (serialKiller == null)
            {
                return NotFound();
            }

            return serialKiller;
        }

        [HttpGet("GetCasualName")]
        public async Task<ActionResult<string>> GetRandomName()
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var NameList = await _context.SerialKillers.ToListAsync();
            int indexRandom = new Random().Next(0, NameList.Count);
            return $"{NameList.ElementAt(indexRandom).Name} {NameList.ElementAt(indexRandom).Surname}";
        }

        [HttpGet("GetNameByFirstLetter/{FirstLetter}")]
        public async Task<ActionResult<string>> GetNameByFirstLetter(char FirstLetter)
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var List = await _context.SerialKillers.ToListAsync();
            foreach (var item in List)
            {
                if (item.Name.ToLower().StartsWith(FirstLetter.ToString().ToLower()))
                {
                    return $"{item.Name} {item.Surname}";
                }
            }
            return NotFound();
        }

        [HttpGet("GetKillersByMaxKill")]
        public async Task<ActionResult<List<SerialKiller>>> GetKillersByMaxKill()
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var List = await _context.SerialKillers.ToListAsync();
            List<int> listKill = new List<int>();
            List<SerialKiller> killers = new List<SerialKiller>();
            foreach (var item in List)
            {
                listKill.Add(item.Killing);
            }
            int Max = listKill.Max();
            foreach (var item in List) 
            {
                if(item.Killing == Max)
                {
                    killers.Add(item);
                }
            }
            if(killers.Count > 0)
            {
                return killers;
            }
            return NotFound();

        }

        [HttpGet("GetKillersByMinKill")]
        public async Task<ActionResult<List<SerialKiller>>> GetKillersByMinKill()
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var List = await _context.SerialKillers.ToListAsync();
            List<int> listKill = new List<int>();
            List<SerialKiller> killers = new List<SerialKiller>();
            foreach (var item in List)
            {
                listKill.Add(item.Killing);
            }
            int Min = listKill.Min();
            foreach (var item in List)
            {
                if (item.Killing == Min)
                {
                    killers.Add(item);
                }
            }
            if (killers.Count > 0)
            {
                return killers;
            }
            return NotFound();
        }

        [HttpGet("GetAvgKill")]
        public async Task<ActionResult<decimal>> GetAvgKill()
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var List = await _context.SerialKillers.ToListAsync();
            decimal sumAvg = 0;
            foreach(var item in List)
            {
                sumAvg = sumAvg + item.Killing;
            }
            return sumAvg/List.Count();

        }

        // PUT: api/SerialKillers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerialKiller(int id, SerialKiller serialKiller)
        {
            if (id != serialKiller.Id)
            {
                return BadRequest();
            }

            _context.Entry(serialKiller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerialKillerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SerialKillers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SerialKiller>> PostSerialKiller(SerialKiller serialKiller)
        {
          if (_context.SerialKillers == null)
          {
              return Problem("Entity set 'DataContext.SerialKillers'  is null.");
          }
            _context.SerialKillers.Add(serialKiller);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSerialKiller", new { id = serialKiller.Id }, serialKiller);
        }

        // DELETE: api/SerialKillers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerialKiller(int id)
        {
            if (_context.SerialKillers == null)
            {
                return NotFound();
            }
            var serialKiller = await _context.SerialKillers.FindAsync(id);
            if (serialKiller == null)
            {
                return NotFound();
            }

            _context.SerialKillers.Remove(serialKiller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SerialKillerExists(int id)
        {
            return (_context.SerialKillers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

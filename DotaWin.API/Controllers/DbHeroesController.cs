using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotaWin.DataAPI;
using DotaWin.Data.Models;

namespace DotaWin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbHeroesController : ControllerBase
    {
        private readonly DotaWinDbContext _context;

        public DbHeroesController(DotaWinDbContext context)
        {
            _context = context;
        }

        // GET: api/DbHeroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbHero>>> GetHeroes()
        {
          if (_context.Heroes == null)
          {
              return NotFound();
          }
            return await _context.Heroes.ToListAsync();
        }

        // GET: api/DbHeroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DbHero>> GetDbHero(int id)
        {
          if (_context.Heroes == null)
          {
              return NotFound();
          }
            var dbHero = await _context.Heroes.FindAsync(id);

            if (dbHero == null)
            {
                return NotFound();
            }

            return dbHero;
        }

        // PUT: api/DbHeroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDbHero(int id, DbHero dbHero)
        {
            if (id != dbHero.Id)
            {
                return BadRequest();
            }

            _context.Entry(dbHero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbHeroExists(id))
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

        // POST: api/DbHeroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DbHero>> PostDbHero(DbHero dbHero)
        {
          if (_context.Heroes == null)
          {
              return Problem("Entity set 'DotaWinDbContext.Heroes'  is null.");
          }
            _context.Heroes.Add(dbHero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDbHero", new { id = dbHero.Id }, dbHero);
        }

        // DELETE: api/DbHeroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDbHero(int id)
        {
            if (_context.Heroes == null)
            {
                return NotFound();
            }
            var dbHero = await _context.Heroes.FindAsync(id);
            if (dbHero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DbHeroExists(int id)
        {
            return (_context.Heroes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

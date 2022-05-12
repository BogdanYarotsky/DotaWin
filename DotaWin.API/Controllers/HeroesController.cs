using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotaWin.Data;
using DotaWin.Data.Models;
using DotaWin.API.Services;

namespace DotaWin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly DotaWinHeroesService _heroesService;

        public HeroesController(DotaWinDbContext context)
        {
            _heroesService = new DotaWinHeroesService(context);
        }

        // GET: api/Heroes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DbHero>>> GetHeroes()
        //{
        //    return await _heroesService.GetHeroesAsync();
        //}

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DotaWinHero>> GetDbHero(string id)
        {
            var hero = await _heroesService.GetHeroInfo(id);
            if (hero is null) return NotFound();
            return hero;
        }

        //// PUT: api/Heroes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDbHero(int id, DbHero dbHero)
        //{
        //    if (id != dbHero.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(dbHero).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DbHeroExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Heroes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<DbHero>> PostDbHero(DbHero dbHero)
        //{
        //  if (_context.Heroes == null)
        //  {
        //      return Problem("Entity set 'DotaWinDbContext.Heroes'  is null.");
        //  }
        //    _context.Heroes.Add(dbHero);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDbHero", new { id = dbHero.Id }, dbHero);
        //}

        //// DELETE: api/Heroes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDbHero(int id)
        //{
        //    if (_context.Heroes == null)
        //    {
        //        return NotFound();
        //    }
        //    var dbHero = await _context.Heroes.FindAsync(id);
        //    if (dbHero == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Heroes.Remove(dbHero);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool DbHeroExists(int id)
        //{
        //    return (_context.Heroes?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}

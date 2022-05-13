using DotaWin.API.Services;
using DotaWin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotaWin.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HeroesController : ControllerBase
{
    private readonly HeroesService _heroesService;

    public HeroesController(DotaWinDbContext db)
    {
        _heroesService = new HeroesService(db);
    }

    [HttpGet]
    public async Task<ActionResult<List<DotaWinHero>>> GetHeroes()
    => await _heroesService.GetHeroes();

    [HttpGet("{id}")]
    public async Task<ActionResult<DotaWinHero>> GetHero(string id)
    {
        var hero = await _heroesService.GetHeroInfo(id);
        if (hero == null) return NotFound();
        return hero;
    }
        
}

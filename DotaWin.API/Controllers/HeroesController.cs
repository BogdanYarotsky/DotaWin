using DotaWin.API.Interfaces;
using DotaWin.API.Models;
using DotaWin.API.Services;
using DotaWin.Data;
using DotaWin.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotaWin.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HeroesController : ControllerBase
{
    private readonly IHeroesService _heroService;
    public HeroesController(IHeroesService heroService)
    {
        _heroService = heroService;
    }

    [HttpGet]
    public async Task<ActionResult<DotaWinHero[]>> GetHeroes() => await _heroService.GetAll();

    [HttpGet("{id}")]
    public async Task<ActionResult<DotaWinHero>> GetHero([FromRoute] string heroName)
    {
        var hero = await _heroService.Get(heroName);
        if (hero == null) return NotFound();
        return hero;
    }
}

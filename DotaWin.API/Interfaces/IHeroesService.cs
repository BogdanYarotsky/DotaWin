using DotaWin.API.Models;
using DotaWin.API.Services;

namespace DotaWin.API.Interfaces;

public interface IHeroesService
{
    public Task<DotaWinHero?> GetHeroInfo(string name);
    public Task<DotaWinHero[]> GetHeroes();
}

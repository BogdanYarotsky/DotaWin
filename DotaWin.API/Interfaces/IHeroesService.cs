using DotaWin.API.Models;
using DotaWin.API.Services;

namespace DotaWin.API.Interfaces;

public interface IHeroesService
{
    public Task<DotaWinHero?> Get(string name);
    public Task<DotaWinHero[]> GetAll();
}

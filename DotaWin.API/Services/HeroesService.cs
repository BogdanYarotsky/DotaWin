using DotaWin.API.Interfaces;
using DotaWin.API.Models;
using DotaWin.API.Utilities;
using DotaWin.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.API.Services;

//public record DotaWinHeroItem(string Name, int Matches, double AddedWinrate, int WinratePer1000Gold);
//public record DotaWinHero(string Name, double Winrate, List<DotaWinHeroItem> Items);

internal class HeroesService : IHeroesService
{
    private readonly DotaWinDbContext _db;

    public HeroesService(DotaWinDbContext db)
    {
        _db = db;
    }

    public async Task<DotaWinHero?> GetHeroInfo(string name)
    {
        var updId = await _db.DailyUpdates
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .Select(x => x.Id)
            .FirstAsync();

        name = name.Equals("Anti-Mage", StringComparison.OrdinalIgnoreCase) ?
            "Anti-Mage" : RouteNameToHeroName(name);

        return await _db.Heroes
            .AsNoTracking()
            .Where(hero => hero.Update.Id == updId && hero.Name == name)
            .Select(hero => new DotaWinHero
            {
                Name = hero.Name,
                Winrate = hero.Winrate,
                Items = hero.HeroItems.Select(i => new DotaWinItem { Id = i.Item.Name }).ToList(),
            })
            .FirstOrDefaultAsync();
    }

    public async Task<DotaWinHero[]> GetHeroes()
    {
        var updId = await _db.DailyUpdates
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .Select(x => x.Id)
            .FirstAsync();

        var heroes = await _db.Heroes
            .Include(h => h.HeroItems)
            .Where(hero => hero.Update.Id == updId)
            .OrderByDescending(hero => hero.Winrate)
            .Select(hero => new DotaWinHero
            {
                Name = hero.Name,
                Winrate = hero.Winrate
            }).ToArrayAsync();

        return heroes;
    }

    private static string RouteNameToHeroName(string routeName) => routeName.ToLower().Replace('-', ' ').ToTitleCase();
}




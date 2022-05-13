using DotaWin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DotaWin.API.Services;

public record DotaWinHeroItem(string Name, int Matches, double AddedWinrate, int WinratePer1000Gold);
public record DotaWinHero(string Name, double Winrate, List<DotaWinHeroItem> Items);

internal class HeroesService
{
    private readonly DotaWinDbContext db;

    public HeroesService(DotaWinDbContext db)
    {
        this.db = db;
    }

    // Get Hero + Items
    public async Task<DotaWinHero?> GetHeroInfo(string name)
    {
        var updId = db.DailyUpdates
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .Select(x => x.Id)
            .First();

        if (name.Equals("Anti-Mage", StringComparison.OrdinalIgnoreCase))
        {
            name = "Anti-Mage";
        }
        else 
        {
            name = RouteNameToHeroName(name);
        }

        return await db.Heroes
            .AsNoTracking()
            .Where(hero => hero.Update.Id == updId && hero.Name == name)
            .Select(hero => new DotaWinHero(hero.Name, hero.Winrate, new List<DotaWinHeroItem>()))
            .FirstOrDefaultAsync();
    }

    internal Task<ActionResult<List<DotaWinHero>>> GetHeroes()
    {
        throw new NotImplementedException();
    }

    private string RouteNameToHeroName(string routeName) => routeName.ToLower().Replace('-', ' ').ToTitleCase();

    // Get Hero list

}


static class Utilities
{
    public static string ToTitleCase(this string title)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
    }
}

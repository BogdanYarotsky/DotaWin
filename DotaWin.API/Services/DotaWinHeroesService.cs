using DotaWin.Data;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.API.Services;

public record DotaWinHero(string Name, double Winrate);

internal class DotaWinHeroesService
{
    private readonly DotaWinDbContext db;

    public DotaWinHeroesService(DotaWinDbContext db)
    {
        this.db = db;
    }

    // Get Hero + Items
    public async Task<DotaWinHero?> GetHeroInfo(string name)
    {
        return await db.Heroes
            .AsNoTracking()
            .Where(hero => hero.Update.Date == DateTime.Now && hero.Name == name)
            .Select(hero => new DotaWinHero(hero.Name, hero.Winrate))
            .FirstOrDefaultAsync();
    }

    // Get Hero list

}

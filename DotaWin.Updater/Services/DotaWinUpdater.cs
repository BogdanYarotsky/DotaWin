using ConsoleTables;
using DotaWin.DataLayer;
using DotaWin.DataLayer.Models;
using DotaWin.Updater.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DotaWin.Updater.Services;

internal class DotaWinUpdater
{
    private readonly DotaWinDbContext _db;
    private readonly DotaAPI _api;

    private readonly string[] boots = { "boots", "greaves", "treads" };
    public enum UpdateResult
    {
        Success, UpToDate, Fail
    }

    public DotaWinUpdater(string apiKey, DbContextOptions<DotaWinDbContext> options)
    {
        _db = new DotaWinDbContext(options);
        _api = new DotaAPI(apiKey);
    }

    public async Task<UpdateResult> RunDailyUpdate()
    {
        var todaysUpdate = await _db.DailyUpdates.SingleOrDefaultAsync(upd => upd.Date == DateTime.Today);
        return todaysUpdate is null ? await UpdateDatabase() : UpdateResult.UpToDate;
    }

    private async Task<UpdateResult> UpdateDatabase()
    {
        var update = new DbUpdate { Date = DateTime.Today };
        var itemsList = await _api.GetItemsAsync();
        var itemsDict = await ItemListToDbItemsMapAsync(itemsList, update);
        var crawler = await DotabuffCrawler.CreateAsync();
        var watch = new Stopwatch();
        watch.Start();
        List<DotabuffHero> heroes = await crawler.GetHeroesAsync();
        watch.Stop();
        Console.WriteLine("Got heroes in " + watch.Elapsed.TotalSeconds + " seconds");
        update.Heroes = MergeHeroesAndItems(heroes, itemsDict, update);
        await _db.AddAsync(update);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return UpdateResult.Fail;
        }

        return UpdateResult.Success;
    }

    private List<DbHero> MergeHeroesAndItems(List<DotabuffHero> heroes, Dictionary<string, DbItem> itemsDict, DbUpdate upd)
    {
        return heroes.Select(hero =>
        {
            var dbHero = new DbHero
            {
                Name = hero.HeroName,
                Winrates = new List<DbHeroWinrate>(),
                HeroItems = new List<DbHeroItem>(),
                Update = upd
            };

            dbHero.Winrates.Add(new DbHeroWinrate { Skill = SkillLevel.Normal, Value = hero.Winrate });

            foreach (var item in hero.Items)
            {
                dbHero.HeroItems.Add(new DbHeroItem
                {
                    Item = itemsDict.GetValueOrDefault(item.Name),
                    Winrate = item.Winrate,
                    Matches = item.Matches,
                    Hero = dbHero,
                    Update = upd
                });
            }

            return dbHero;
        }).ToList();
    }

    private async Task<Dictionary<string, DbItem>> ItemListToDbItemsMapAsync(DotaItemsResult dotaItems, DbUpdate update)
    {
        return dotaItems.items
            .Where(i => i.recipe != 1 && i.localized_name != "Necronomicon")
            .Select(i => TransformToDbItem(i, update))
            .ToDictionary(i => i.Name, i => i);
    }

    private DbItem TransformToDbItem(Item i, DbUpdate upd)
    {
        if (i.localized_name == "Dagon")
        {
            var level = i.name.Last();
            if (level != 'n')
            {
                i.localized_name = $"Dagon (level {level})";
            }
        }

        DbItem.Type itemType;
        if (i.cost <= 0)
        {
            itemType = DbItem.Type.Neutral;
        }
        else if (boots.Any(b => i.localized_name.Contains(b, StringComparison.OrdinalIgnoreCase)))
        {
            itemType = DbItem.Type.Boots;
        }
        else { itemType = DbItem.Type.Core; }

        return new DbItem
        {
            Name = i.localized_name,
            Price = i.cost,
            ItemType = itemType,
            TechnicalName = i.name
        };
    }
}

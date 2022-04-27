using ConsoleTables;
using DotaWin.Data.Models;
using DotaWin.Updater.Utilities;

string[] boots = { "boots", "greaves", "treads" };
// i.localized_name.Contains("boots", StringComparison.OrdinalIgnoreCase)

var dotaApi = new DotaAPI("B479F4855E8EC7C228DF9045FA77978B");
var itemsList = await dotaApi.GetItemsAsync();
var dbItems = itemsList.items.Where(i => i.recipe != 1).Select(
    i =>
    {
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
            ImgUrl = $"http://cdn.dota2.com/apps/dota2/images/items/{i.name.Replace("item_", "")}_lg.png"        
        };
    }).OrderBy(i => i.ItemType);

ConsoleTable
    .From(dbItems)
    .Configure(o => o.NumberAlignment = Alignment.Right)
    .Write(Format.Alternative);

//await using var crawler = await DotabuffCrawler.CreateAsync();
//var heroes = await crawler.GetHeroesAsync();
//foreach (var hero in heroes) Console.WriteLine(hero.HeroName);
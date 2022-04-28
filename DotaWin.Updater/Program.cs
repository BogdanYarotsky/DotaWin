using ConsoleTables;
using DotaWin.Data.Models;
using DotaWin.Updater.Utilities;

string[] boots = { "boots", "greaves", "treads" };

var dotaApi = new DotaAPI("B479F4855E8EC7C228DF9045FA77978B");
var itemsList = await dotaApi.GetItemsAsync();
var dbItemsMap = itemsList.items.Where(i => i.recipe != 1 && i.localized_name != "Necronomicon").Select(
    i =>
    {
        // handle dagon exception
        if (i.localized_name == "Dagon")
        {
            var level = i.name.Last();
            if (level != 'n')
            {
                i.localized_name = $"Dagon (level {level})";
            }
            Console.WriteLine(i.localized_name);
            Console.WriteLine(i.name);
        }

        // figure out the type
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

        // big nice item object
        return new DbItem
        { 
            Name = i.localized_name,
            Price = i.cost,
            ItemType = itemType,
            ImgUrl = $"http://cdn.dota2.com/apps/dota2/images/items/{i.name.Replace("item_", "")}_lg.png"        
        };
    }).ToDictionary(i => i.Name, i => i);

var crawler = await DotabuffCrawler.CreateAsync();
var heroes = await crawler.GetHeroesAsync();

var dbHeroesList = heroes.Select(hero =>
{
    var dbHero = new DbHero
    {
        Name = hero.HeroName,
        Winrate = hero.Winrate,
        HeroItems = new List<DbHeroItem>()
    };

    foreach (var item in hero.Items)
    {
        dbHero.HeroItems.Add(new DbHeroItem
        {
            Item = dbItemsMap.GetValueOrDefault(item.Name),
            Winrate = item.Winrate,
            Matches = item.Matches,
            Hero = dbHero
        });
    }

    return dbHero;
}).ToList();


var exampleHero = dbHeroesList[2];
Console.WriteLine(exampleHero.Name);
ConsoleTable.From(exampleHero.HeroItems).Write();
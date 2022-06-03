using ConsoleTables;
using DotaWin.DataLayer;
using DotaWin.DataLayer.Models;
using DotaWin.Updater.Services;
using Microsoft.EntityFrameworkCore;

var builder = new DbContextOptionsBuilder<DotaWinDbContext>();
var connString = Environment.GetEnvironmentVariable("hello");
builder.UseNpgsql(connString);
using var db = new DotaWinDbContext(builder.Options);
//db.RemoveRange(db.DailyUpdates);
//db.SaveChanges();
//return;

var updater = new DotaWinUpdater("B479F4855E8EC7C228DF9045FA77978B", builder.Options);

//using var filestream = new FileStream("out.txt", FileMode.Create);
//using var streamwriter = new StreamWriter(filestream);
//streamwriter.AutoFlush = true;
//Console.SetOut(streamwriter);
//Console.SetError(streamwriter);

var updateResult = await updater.RunDailyUpdate();


//var items = db.HeroItems.Where(i => i.Item.Name == "Power Treads").Select(i => new 
//{ 
//    Item = i.Item.Name, 
//    i.Hero.Name,
//    i.Winrate
//});

//Console.WriteLine(items.Count());
//ConsoleTable.From(items.OrderByDescending(i => i.Winrate)).Write();




var abbadon = await db.Heroes
    .AsNoTracking()
    .Where(h => h.Name == "Pugna" && h.Update.Date == DateTime.Today)
    .Select(h => new
    {
        h.Name,
        h.Winrates,
        Items = h.HeroItems.Select(i => new
        {
            i.Item.Name,
            i.Item.ItemType,
            i.Item.Price,
            i.Matches,
            i.Winrate,
            AddedWinrate = Math.Round(i.Winrate - h.Winrates.First().Value, 2),
        })
    })
    .FirstAsync();

Console.WriteLine("Hero: " + abbadon.Name);
Console.WriteLine("Winrate: " + abbadon.Winrates.First().Value);
var items = abbadon.Items
    .Where(i => i.Price > 500 && i.AddedWinrate > 0 && i.ItemType == DbItem.Type.Core)
    .Select(i => new { i.Name, i.Matches, i.AddedWinrate, WinratePer1000Gold = Math.Round(i.AddedWinrate / i.Price * 1000, 2) })
    .OrderByDescending(item => item.Matches);
ConsoleTable.From(items).Write();


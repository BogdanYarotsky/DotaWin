using DotaWin.Data;
using DotaWin.Data.Models;


Console.WriteLine("Hello, World!");
using var db = new DotaWinDbContext();
var upd = new Update { Date = DateOnly.FromDateTime(DateTime.Now) };
await db.AddAsync(upd);
var hero = new Hero{ Name = "Slark", Winrate = 50.00, Update = upd };
await db.AddAsync(hero);
await db.SaveChangesAsync();

var savedHero = db.Heroes.FirstOrDefault();
if (savedHero != null)
{
    Console.WriteLine(savedHero.Name + " successful");
    db.Heroes.Remove(savedHero);
    db.Updates.Remove(upd);
    await db.SaveChangesAsync();
}
else
{
    Console.WriteLine("test unsuccessful");
}




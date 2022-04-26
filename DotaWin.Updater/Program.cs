using DotaWin.Data;
using DotaWin.Data.Models;
using DotaWin.Updater.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;


var heroesList = await DotaAPI.GetHeroes();
Console.WriteLine(heroesList.count);
foreach (var hero in heroesList.heroes)
{
    Console.WriteLine(hero.localized_name);
}

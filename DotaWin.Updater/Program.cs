using DotaWin.Data;
using DotaWin.Data.Models;
using DotaWin.Updater.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;


await using var crawler = await DotabuffCrawler.CreateAsync();
Console.WriteLine(await crawler.GetPatchAsync());

using DotaWin.Data;
using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;

var patchSelector = "#dota_react_root > div > div > div.patchnotespage_Header_2uAz0 > div.patchnotespage_NotesTitle_oyfUT";

//https://stackoverflow.com/questions/70138360/is-there-a-way-to-iterate-over-a-li-list-in-playwright-and-click-over-each-ele

// Step 0 - check if there were a succesful update already
using var db = new DotaWinDbContext();
var todaysDate = DateTime.Today;
var lastDbUpdate = await db.MostRecentUpdateAsync();

if (lastDbUpdate != null && lastDbUpdate.Date == todaysDate)
{
    Console.WriteLine("All is good, nothing to update");
    return;
}

// Step 1 - get today's Dota patch string
Console.WriteLine("Time to get today's data");
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync();
var page = await browser.NewPageAsync();
await page.GotoAsync("https://www.dota2.com/patches");
var todaysPatch = await page.Locator(patchSelector).TextContentAsync();

if (todaysPatch != lastDbUpdate.Patch)
{
    // get heroes and items from Dev API
}


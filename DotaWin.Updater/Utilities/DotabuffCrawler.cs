using DotaWin.Updater.Models;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Updater.Utilities
{
    public sealed class DotabuffCrawler : IAsyncDisposable, IDisposable
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private DotabuffCrawler() { }
        private async Task<DotabuffCrawler> StartBrowserAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync();
            _page = await _browser.NewPageAsync();
            return this;
        }

        public async Task<string> GetPatchAsync()
        {
            await _page.GotoAsync("https://www.dota2.com/patches");
            var element = _page.Locator("#dota_react_root > div > div > div.patchnotespage_Header_2uAz0 > div.patchnotespage_NotesTitle_oyfUT");
            return await element.InnerTextAsync();
        }

        public async Task<List<DotabuffHero>> GetHeroesAsync()
        {
            var heroes = new List<DotabuffHero>();
            
            // ordered first
            // parallel later

            return heroes;
        }

        public static async Task<DotabuffCrawler> CreateAsync()
        {
            var crawler = new DotabuffCrawler();
            return await crawler.StartBrowserAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _browser.DisposeAsync();
           _playwright.Dispose();
        }

        public void Dispose()
        {
            _browser.DisposeAsync().GetAwaiter().GetResult();
            _playwright.Dispose();
        }
    }
}

//var patchSelector = "#dota_react_root > div > div > div.patchnotespage_Header_2uAz0 > div.patchnotespage_NotesTitle_oyfUT";

////https://stackoverflow.com/questions/70138360/is-there-a-way-to-iterate-over-a-li-list-in-playwright-and-click-over-each-ele

//// Step 0 - check if there were a succesful update already
//using var db = new DotaWinDbContext();
//var todaysDate = DateTime.Today;
//var lastDbUpdate = await db.MostRecentUpdateAsync();

//if (lastDbUpdate != null && lastDbUpdate.Date == todaysDate)
//{
//    Console.WriteLine("All is good, nothing to update");
//    return;
//}

//// Step 1 - get today's Dota patch string
//Console.WriteLine("Time to get today's data");


//await page.GotoAsync("https://www.dota2.com/patches");
//var todaysPatch = await page.Locator(patchSelector).TextContentAsync();

//if (todaysPatch != lastDbUpdate.Patch)
//{
//    // get heroes and items from Dev API
//}
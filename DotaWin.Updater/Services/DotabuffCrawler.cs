using DotaWin.Updater.Models;
using Microsoft.Playwright;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Updater.Services;

internal sealed class DotabuffCrawler : IAsyncDisposable, IDisposable
{
    private const string patchSelector = "#dota_react_root > div > div > div.patchnotespage_Header_2uAz0 > div.patchnotespage_NotesTitle_oyfUT";
    private const string cookiesBtnSelector = "body > div.fc-consent-root > div.fc-dialog-container > div.fc-dialog.fc-choice-dialog > div.fc-footer-buttons-container > div.fc-footer-buttons > button.fc-button.fc-cta-consent.fc-primary-button";
    private const string heroesListSelector = "body > div.container-outer.seemsgood > div.skin-container > div.container-inner.container-inner-content > div.content-inner > section:nth-child(3) > footer > div > a";
    private const string heroItemsListSelector = "body > div.container-outer.seemsgood > div.skin-container > div.container-inner.container-inner-content > div.content-inner > section > article > table > tbody > tr";
    private const string heroWinrateSelector = "body > div.container-outer.seemsgood > div.skin-container > div.container-inner.container-inner-content > div.header-content-container > div.header-content > div.header-content-secondary > dl:nth-child(2) > dd > span";

    private readonly BrowserTypeLaunchPersistentContextOptions _options = new BrowserTypeLaunchPersistentContextOptions { Headless = true };
    private IPlaywright _playwright;
    private IBrowserContext _browser;
    private IPage _page;
    private readonly int _chunkSize = 7;

    private DotabuffCrawler() { }
    private async Task<DotabuffCrawler> StartBrowserAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchPersistentContextAsync(Directory.GetCurrentDirectory() + "temp", _options);
        _page = await _browser.NewPageAsync();
        return this;
    }

    public async Task<string> GetPatchAsync()
    {
        await _page.GotoAsync("https://www.dota2.com/patches");
        return await _page.Locator(patchSelector).InnerTextAsync();
    }

    public async Task<List<DotabuffHero>> GetHeroesAsync()
    {
        List<TimeSpan> _extractionTimes = new();
        await _page.GotoAsync("https://www.dotabuff.com/heroes");
        var heroIcons = _page.Locator(heroesListSelector);
        var totalHeroes = await heroIcons.CountAsync();

        // get links of every hero
        var heroUrl = new UriBuilder("https", "www.dotabuff.com");
        var urls = new List<Uri>();
        for (var i = 0; i < totalHeroes; i++)
        {
            var href = await heroIcons.Nth(i).GetAttributeAsync("href");
            heroUrl.Path = href + "/items";
            urls.Add(heroUrl.Uri);
        }

        // all info goes here
        var bag = new ConcurrentBag<DotabuffHero>();
        var tasks = urls.Select(async url => bag.Add(await ExtractDotabuffHeroInfo(url, _extractionTimes)));

        foreach (var chunk in tasks.Chunk(_chunkSize))
        {
            await Task.WhenAll(chunk);
        }

        Console.WriteLine($"Average time to extract 1 hero: {_extractionTimes.Select(i => i.TotalSeconds).Average()} seconds");
        return bag.ToList();
    }

    private async Task<DotabuffHero> ExtractDotabuffHeroInfo(Uri heroUrl, List<TimeSpan> extractionTimes)
    {
        var watch = new Stopwatch();
        watch.Start();

        var page = await _browser.NewPageAsync();
        await page.RouteAsync("**/*", async r =>
        {
            if (r.Request.ResourceType == "image") await r.AbortAsync();
            else await r.ContinueAsync();
        });

        Console.WriteLine("Extracting " + heroUrl);

        await page.GotoAsync(heroUrl.ToString(), new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
        string name;

        try 
        {
            name = await page.Locator("h1").InnerTextAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Oops, cought an exception" + ex.ToString());
            Console.WriteLine("I assume it is because of the cookies check. Trying to solve.");
            watch.Stop(); 
            Console.WriteLine("Exact duration in seconds: " + watch.Elapsed.TotalSeconds);
            await page.Locator(cookiesBtnSelector).ClickAsync();
            name = await page.Locator("h1").InnerTextAsync();
        }

        // get winrate
        var heroWinrate = await page.Locator(heroWinrateSelector).InnerTextAsync();
        heroWinrate = heroWinrate.Replace("%", "").Trim();
        // get items

        var items = new List<DotabuffItem>();
        var heroList = page.Locator(heroItemsListSelector);
        var itemCount = await heroList.CountAsync();
        for (int i = 0; i < itemCount; i++)
        {
            var row = heroList.Nth(i);
            var columns = row.Locator("td");
            var itemName = await columns.Nth(1).InnerTextAsync();
            if (itemName.StartsWith("Recipe")) continue;
            var matches = int.Parse(await columns.Nth(2).GetAttributeAsync("data-value"));
            if (matches < 9001) continue; // sample is too small
            var winrate = double.Parse(await columns.Nth(3).GetAttributeAsync("data-value"));
            winrate = Math.Round(winrate, 2);
            items.Add(new DotabuffItem
            {
                Name = itemName,
                Winrate = winrate,
                Matches = matches
            });
        }

        await page.CloseAsync();

        watch.Stop();
        var heroName = name.Replace("Items", "").Trim();
        Console.WriteLine($"Extracted {heroName} in {watch.Elapsed}");
        extractionTimes.Add(watch.Elapsed);
        return new DotabuffHero { HeroName = heroName, Winrate = double.Parse(heroWinrate), Items = items };
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
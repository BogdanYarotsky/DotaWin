using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DotaWin.Updater.Utilities;
using System.Linq;
using System;
using System.Text.RegularExpressions;

namespace DotaWin.Test
{
    [TestClass]
    public class DotabuffCrawlerTests : IAsyncDisposable
    {
        private DotabuffCrawler crawler;
        public async ValueTask DisposeAsync() => await crawler.DisposeAsync();

        [TestInitialize]
        public async Task InitCrawler() => crawler = await DotabuffCrawler.CreateAsync();

        [TestMethod]
        public async Task TestPatchVersion()
        {
            var patch = await crawler.GetPatchAsync();
            var withoutLetters = Regex.Replace(patch, "[^0-9.]", "");
            Console.WriteLine(withoutLetters);
            Assert.IsTrue(double.TryParse(withoutLetters, out var _));
        }
    }
}

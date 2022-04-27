using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DotaWin.Updater.Utilities;
using System.Linq;

namespace DotaWin.Test
{
    [TestClass]
    public class DotaAPITest
    {
        private readonly DotaAPI dotaAPI = new("B479F4855E8EC7C228DF9045FA77978B");

        [TestMethod]
        public async Task TestItemsFetch()
        {
            var itemList = await dotaAPI.GetItems();
            Assert.AreEqual(200, itemList.status);
            Assert.IsTrue(itemList.items.Count() > 5);
        }

        [TestMethod]
        public async Task TestHeroesFetch()
        {
            var heroesList = await dotaAPI.GetHeroes();
            Assert.AreEqual(200, heroesList.status);
            Assert.IsTrue(heroesList.heroes.Count() > 5);
        }
    }
}
using DotaWin.Updater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DotaWin.Updater.Utilities
{
    public static class DotaAPI
    {
        private static readonly HttpClient client = new();
        public static async Task<DotaHeroesResult> GetHeroes()
        {
            var jsonString = await client.GetStringAsync("https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v1/?format=JSON&language=en_us&key=B479F4855E8EC7C228DF9045FA77978B");
            var obj = JsonSerializer.Deserialize<DotaHeroesList>(jsonString);
            return obj.result;
        }

        internal static async Task<DotaItemsResult> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}

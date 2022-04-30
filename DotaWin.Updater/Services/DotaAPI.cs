using DotaWin.Updater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace DotaWin.Updater.Services
{
    internal class DotaAPI
    {
        private static readonly HttpClient _client = new();
        private readonly string _apiKey;
        public DotaAPI(string apiKey)
        {
            _apiKey = apiKey;
        }
        public async Task<DotaHeroesResult> GetHeroesAsync()
        {
            var url = GetAPILink("GetHeroes");
            var jsonString = await _client.GetStringAsync(url);
            var obj = JsonSerializer.Deserialize<DotaHeroesList>(jsonString);
            return obj.result;
        }

        public async Task<DotaItemsResult> GetItemsAsync()
        {
            var url = GetAPILink("GetGameItems");
            var jsonString = await _client.GetStringAsync(url);
            var obj = JsonSerializer.Deserialize<DotaItemsList>(jsonString);
            return obj.result;
        }

        private string GetAPILink(string query)
            => $"https://api.steampowered.com/IEconDOTA2_570/{query}/v1/?format=JSON&language=en_us&key={_apiKey}";
    }
}

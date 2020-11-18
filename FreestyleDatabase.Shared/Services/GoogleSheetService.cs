using FreestyleDatabase.Shared.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class GoogleSheetService
    {
        private const string baseUrl = "https://spreadsheets.google.com/feeds/list/1pdacjxVJNQRfF5s0LZ-J2w22d0tgjTz1a9F7l5rbddc/od6/public/values?alt=json";
        private static GoogleSheetResponseModel cache;
        private static DateTime cacheTime;
        private readonly HttpClient http;

        public GoogleSheetService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<GoogleSheetResponseModel> GetSheetAsync(CancellationToken cancellationToken = default)
        {
            if (cache != null)
            {
                if (cacheTime < DateTime.Now.AddMinutes(1))
                {
                    return cache;
                }
                else
                {
                    cache = null;
                }
            }

            var request = await http.GetAsync(baseUrl, cancellationToken);

            if (!request.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await request.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GoogleSheetResponseModel>(response);

            cache = result;
            cacheTime = DateTime.Now;

            return result;
        }
    }
}
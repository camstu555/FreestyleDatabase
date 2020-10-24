using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using rankings2.Models;

namespace rankings2.Services
{
    public class GoogleSheetService
    {
        private const string baseUrl = "https://spreadsheets.google.com/feeds/list/1pdacjxVJNQRfF5s0LZ-J2w22d0tgjTz1a9F7l5rbddc/od6/public/values?alt=json";
        private readonly HttpClient http;

        public GoogleSheetService(HttpClient http)
        {
            this.http = http;
        }
        public async Task<GoogleSheetResponseModel> GetSheetAsync(CancellationToken cancellationToken = default)
        {
            var request = await this.http.GetAsync(baseUrl, cancellationToken);

            if (!request.IsSuccessStatusCode)
            {
                return null;
            }

            var response = await request.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GoogleSheetResponseModel>(response);
        }
    }
}

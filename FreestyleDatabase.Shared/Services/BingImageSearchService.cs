using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class BingImageSearchService
    {
        private const string Access = "4a56b20a226b4995be075ebca1832ec0";
        private const string HeaderName = "Ocp-Apim-Subscription-Key";
        private const string Url = "https://api.bing.microsoft.com/v7.0/images/search?q={0}+wrestling";
        private readonly HttpClient httpClient;
        private readonly Dictionary<string, BingResultItem> cache = new Dictionary<string, BingResultItem>();

        public BingImageSearchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetWrestlerImageResult(string wrestlerName)
        {
            var json = await GetWrestlerSearchResult(wrestlerName);

            return JsonConvert.SerializeObject(json);
        }

        public async Task<string> GetWrestlerImageResultRaw(string wrestlerName)
        {
            var json = await GetWrestlerSearchResult(wrestlerName);

            return json.ContentUrl;
        }

        public async Task<(byte[], string)> GetWrestlerImageResultBytes(string wrestlerName)
        {
            var wrestler = await GetWrestlerSearchResult(wrestlerName);
            var message = new HttpRequestMessage(HttpMethod.Get, wrestler.ContentUrl);
            var response = await httpClient.SendAsync(message);

            using var ms = new MemoryStream();
            using var rs = await response.Content.ReadAsStreamAsync();

            await rs.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return (ms.ToArray(), wrestler.EncodingFormat);
        }

        private async Task<BingResultItem> GetWrestlerSearchResult(string wrestlerName)
        {
            if (string.IsNullOrEmpty(wrestlerName))
            {
                throw new InvalidOperationException("Wrestler name must be provided.");
            }

            if (cache.ContainsKey(wrestlerName))
            {
                return cache[wrestlerName];
            }

            var route = string.Format(Url, wrestlerName);
            var message = new HttpRequestMessage(HttpMethod.Get, route);

            message.Headers.TryAddWithoutValidation(HeaderName, Access);

            var request = await httpClient.SendAsync(message);

            if (!request.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Could not find anything...");
            }

            var response = await request.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<BingResult>(response);
            var result = json.Value.First();

            cache[wrestlerName] = result;

            return result;
        }
    }

    internal class BingResult
    {
        public List<BingResultItem> Value { get; set; }
    }

    internal class BingResultItem
    {
        public string WebSearchUrl { get; set; }
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public string ContentUrl { get; set; }
        public string HostPageUrl { get; set; }
        public string ContentSize { get; set; }
        public string EncodingFormat { get; set; }
        public string HostPageDisplayUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string HostPageFavIconUrl { get; set; }
        public string HostPageDomainFriendlyName { get; set; }
        public BingResultImageThumbnail Thumbnail { get; set; }
        public string ImageId { get; set; }
        public string AccentColor { get; set; }
    }

    internal class BingResultImageThumbnail
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
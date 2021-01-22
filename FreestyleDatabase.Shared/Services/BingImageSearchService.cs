using FreestyleDatabase.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace FreestyleDatabase.Shared.Services
{
    public class BingImageSearchService
    {
        private const string Access = "6551be3838d04560bb319fa43a1d3960";
        private const string HeaderName = "Ocp-Apim-Subscription-Key";
        private const string Url = "https://api.bing.microsoft.com/v7.0/images/search?q={0}+wrestling";
        private readonly HttpClient httpClient;
        private readonly AzureSearchService azureSearchService;
        private readonly StorageAccountService storageAccountService;

        public BingImageSearchService(HttpClient httpClient, AzureSearchService azureSearchService, StorageAccountService storageAccountService)
        {
            this.httpClient = httpClient;
            this.azureSearchService = azureSearchService;
            this.storageAccountService = storageAccountService;
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

        public async Task<(byte[], string)> GetWrestlerImageResultBytes(string wrestlerName, string wrestlerId)
        {
            var wrestler = await GetWrestlerSearchResult(wrestlerName);
            var message = new HttpRequestMessage(HttpMethod.Get, wrestler.ContentUrl);
            var response = await httpClient.SendAsync(message);

            using var ms = new MemoryStream();
            using var rs = await response.Content.ReadAsStreamAsync();

            await rs.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            if (!string.IsNullOrEmpty(wrestlerId))
            {
                {
                    var query = new Uri($"https://d.com/dummy?$filter=WrestlerId1 eq '{wrestlerId}'");

                    var wrestlerResults = await azureSearchService.Search<List<WrestlingDataModel>>(query);

                    var fileName = $"{wrestlerId}.{wrestler.EncodingFormat}";

                    if (!await storageAccountService.HasFile(fileName))
                    {
                        await storageAccountService.SaveFile(fileName, ms);
                    }

                    var newHref = $"https://freestyledb.azurewebsites.net/api/FreeStyleImageFetcher2?storage={HttpUtility.UrlEncode(fileName)}";
                    var hasMoreThan1RecordUpdated = false;

                    foreach (var w in wrestlerResults)
                    {
                        if (w.WrestlerImage1 != null && w.WrestlerImage1.Equals(newHref, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        w.WrestlerImage1 = newHref;
                        hasMoreThan1RecordUpdated = true;
                    }

                    if (hasMoreThan1RecordUpdated)
                    {
                        await azureSearchService.UpdateDocument(wrestlerResults);
                    }
                }

                {
                    var query = new Uri($"https://d.com/dummy?$filter=WrestlerId2 eq '{wrestlerId}'");

                    var wrestlerResults = await azureSearchService.Search<List<WrestlingDataModel>>(query);

                    var fileName = $"{wrestlerId}.{wrestler.EncodingFormat}";

                    if (!await storageAccountService.HasFile(fileName))
                    {
                        await storageAccountService.SaveFile(fileName, ms);
                    }

                    var newHref = $"https://freestyledb.azurewebsites.net/api/FreeStyleImageFetcher2?storage={HttpUtility.UrlEncode(fileName)}";
                    var hasMoreThan1RecordUpdated = false;

                    foreach (var w in wrestlerResults)
                    {
                        if (w.WrestlerImage2 != null && w.WrestlerImage2.Equals(newHref, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        w.WrestlerImage2 = newHref;
                        hasMoreThan1RecordUpdated = true;
                    }

                    if (hasMoreThan1RecordUpdated)
                    {
                        await azureSearchService.UpdateDocument(wrestlerResults);
                    }
                }
            }

            return (ms.ToArray(), wrestler.EncodingFormat);
        }

        public async Task<(byte[], string)> GetWrestlerThumbailResultBytes(string wrestlerName, string wrestlerId)
        {
            var wrestler = await GetWrestlerSearchResult(wrestlerName);
            var message = new HttpRequestMessage(HttpMethod.Get, wrestler.ThumbnailUrl);
            var response = await httpClient.SendAsync(message);

            using var ms = new MemoryStream();
            using var rs = await response.Content.ReadAsStreamAsync();

            await rs.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            if (!string.IsNullOrEmpty(wrestlerId))
            {
                {
                    var query = new Uri($"https://d.com/dummy?$filter=WrestlerId1 eq '{wrestlerId}'");

                    var wrestlerResults = await azureSearchService.Search<List<WrestlingDataModel>>(query);

                    var fileName = $"{wrestlerId}-thumbnail.{wrestler.EncodingFormat}";

                    if (!await storageAccountService.HasFile(fileName))
                    {
                        await storageAccountService.SaveFile(fileName, ms);
                    }

                    var newHref = $"https://freestyledb.azurewebsites.net/api/FreeStyleImageFetcher2?storage={HttpUtility.UrlEncode(fileName)}";
                    var hasMoreThan1RecordUpdated = false;

                    foreach (var w in wrestlerResults)
                    {
                        if (w.WrestlerThumbnail1 != null && w.WrestlerThumbnail1.Equals(newHref, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        w.WrestlerThumbnail1 = newHref;
                        hasMoreThan1RecordUpdated = true;
                    }

                    if (hasMoreThan1RecordUpdated)
                    {
                        await azureSearchService.UpdateDocument(wrestlerResults);
                    }
                }

                {
                    var query = new Uri($"https://d.com/dummy?$filter=WrestlerId2 eq '{wrestlerId}'");

                    var wrestlerResults = await azureSearchService.Search<List<WrestlingDataModel>>(query);

                    var fileName = $"{wrestlerId}-thumbnail.{wrestler.EncodingFormat}";

                    if (!await storageAccountService.HasFile(fileName))
                    {
                        await storageAccountService.SaveFile(fileName, ms);
                    }

                    var newHref = $"https://freestyledb.azurewebsites.net/api/FreeStyleImageFetcher2?storage={HttpUtility.UrlEncode(fileName)}";
                    var hasMoreThan1RecordUpdated = false;

                    foreach (var w in wrestlerResults)
                    {
                        if (w.WrestlerThumbnail2 != null && w.WrestlerThumbnail2.Equals(newHref, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        w.WrestlerThumbnail2 = newHref;
                        hasMoreThan1RecordUpdated = true;
                    }

                    if (hasMoreThan1RecordUpdated)
                    {
                        await azureSearchService.UpdateDocument(wrestlerResults);
                    }
                }
            }

            return (ms.ToArray(), wrestler.EncodingFormat);
        }

        public async Task<(Stream, string)> GetWrestlerImageResultStream(string wrestlerName)
        {
            var wrestler = await GetWrestlerSearchResult(wrestlerName);
            var message = new HttpRequestMessage(HttpMethod.Get, wrestler.ContentUrl);
            var response = await httpClient.SendAsync(message);

            var ms = new MemoryStream();
            using var rs = await response.Content.ReadAsStreamAsync();

            await rs.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return (ms, wrestler.EncodingFormat);
        }

        private async Task<BingResultItem> GetWrestlerSearchResult(string wrestlerName)
        {
            if (string.IsNullOrEmpty(wrestlerName))
            {
                throw new InvalidOperationException("Wrestler name must be provided.");
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
using FreestyleDatabase.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class WrestlerSearchService
    {
        private const string baseAddress = "https://freestyledb.azurewebsites.net/api/{0}";
        private readonly HttpClient httpClient;

        public WrestlerSearchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<SearchCollectionResponseModel<WrestlingDataModel>> SearchWrestlers(string search = null, int? top = null, int? skip = null)
        {
            var service = "FreeStylesearch";
            var request = CreateRequest(service, search, top, skip);

            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchCollectionResponseModel<WrestlingDataModel>>(json);
        }

        private HttpRequestMessage CreateRequest(string service, string search = null, int? top = null, int? skip = null)
        {
            var route = string.Format(baseAddress, service);

            if (!string.IsNullOrEmpty(search))
            {
                route = route + "?search=" + Uri.EscapeDataString(search);
            }

            if (top.HasValue)
            {
                if (route.Contains("?"))
                {
                    route = route + "&$top=" + top.Value;
                }
                else
                {
                    route = route + "?$top=" + top.Value;
                }
            }

            if (skip.HasValue)
            {
                if (route.Contains("?"))
                {
                    route = route + "&$skip=" + skip.Value;
                }
                else
                {
                    route = route + "?$skip=" + skip.Value;
                }
            }

            return new HttpRequestMessage(HttpMethod.Get, route);
        }
    }
}
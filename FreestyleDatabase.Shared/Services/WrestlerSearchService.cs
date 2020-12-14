using FreestyleDatabase.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
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

        public async Task<SearchCollectionResponseModel<WrestlingDataModel>> SearchWrestlers(string search = null, int? top = null, int? skip = null, string filter = null, string orderBy = null)
        {
            var service = "FreeStylesearch";
            var request = CreateRequest(service, search, top, skip, filter, orderBy);

            var response = await httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchCollectionResponseModel<WrestlingDataModel>>(json);
        }

        public Task<SearchCollectionResponseModel<WrestlingDataModel>> GetAllWrestlerMatches(string wresterId)
        {
            return SearchWrestlers(
                filter: $"{nameof(WrestlingDataModel.WrestlerId1)} eq '{wresterId}' or {nameof(WrestlingDataModel.WrestlerId2)} eq '{wresterId}'",
                top: 1000
            );
        }

        public async Task<WrestlingAggregatesModel> GetWrestlerDetails(string wresterId, int recentMatchCount = 10)
        {
            var wrestlerResults = await GetAllWrestlerMatches(wresterId);
            var result = new WrestlingAggregatesModel();

            var firstResult = wrestlerResults.Items.FirstOrDefault();
            var isWrestler1 = false;

            if (firstResult.WrestlerId1.Equals(wresterId, StringComparison.OrdinalIgnoreCase))
            {
                isWrestler1 = true;
            }

            result.WrestlerId = wresterId;

            if (isWrestler1)
            {
                result.WrestlerName = firstResult.WrestlerName1;
                result.WrestlerImageUrl = firstResult.WrestlerImage1;
            }
            else
            {
                result.WrestlerName = firstResult.WrestlerName2;
                result.WrestlerImageUrl = firstResult.WrestlerImage2;
            }

            var recentMatches = wrestlerResults.Items.Take(recentMatchCount).ToList();

            foreach (var recentMatch in recentMatches)
            {
                result.MostRecentMatches.Items.Add(recentMatch);
            }

            result.Wins = wrestlerResults.Items.Count(x => x.WrestlerId1.Equals(wresterId));
            result.Losses = wrestlerResults.Items.Count(x => x.WrestlerId2.Equals(wresterId));
            result.Pins = wrestlerResults.Items.Count(x => x.WrestlerId1.Equals(wresterId) && x.Result.Equals("VFA", StringComparison.OrdinalIgnoreCase));
            result.Techs = wrestlerResults.Items.Count(x => x.WrestlerId1.Equals(wresterId) && (x.Result.Equals("VSU", StringComparison.OrdinalIgnoreCase) || x.Result.Equals("VSU1", StringComparison.OrdinalIgnoreCase)));
            result.Points = wrestlerResults.Items.Count(x => x.WrestlerId1.Equals(wresterId) && (x.Result.Equals("VPO", StringComparison.OrdinalIgnoreCase) || x.Result.Equals("VPO1", StringComparison.OrdinalIgnoreCase)));

            // TODO: NEED CAM!!!
            result.GoldMedalMatches.AddRange(wrestlerResults.Items.Where(x => x.Round == "huh?").Select(x => x.Venue));
            result.SilverMedalMatches.AddRange(wrestlerResults.Items.Where(x => x.Round == "huh?").Select(x => x.Venue));
            result.BronzeMedalMatches.AddRange(wrestlerResults.Items.Where(x => x.Round == "huh?").Select(x => x.Venue));

            // TODO: NEED CAM!!!
            result.AverageDefensivePointsPerMatch = 0;
            result.AverageOffensivePointsPerMatch = 0;

            return result;
        }

        private HttpRequestMessage CreateRequest(string service, string search = null, int? top = null, int? skip = null, string filter = null, string orderBy = null)
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

            if (!string.IsNullOrEmpty(filter))
            {
                if (route.Contains("?"))
                {
                    route = route + "&$filter=" + Uri.EscapeDataString(filter);
                }
                else
                {
                    route = route + "?$filter=" + Uri.EscapeDataString(filter);
                }
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                if (route.Contains("?"))
                {
                    route = route + "&$orderby=" + Uri.EscapeDataString(orderBy);
                }
                else
                {
                    route = route + "?$orderby=" + Uri.EscapeDataString(orderBy);
                }
            }

            return new HttpRequestMessage(HttpMethod.Get, route);
        }
    }
}
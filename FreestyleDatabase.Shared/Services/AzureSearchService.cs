using FreestyleDatabase.Shared.Extensions;
using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class AzureSearchService
    {
        private const string IndexName = "wrestling-data";
        private readonly HttpClient httpClient;
        private readonly string Endpoint;
        private readonly string Access;
        private readonly string QueryAccess;
        private readonly string Version;

        public AzureSearchService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            Access = "12FC378B9E1FD7DC6C2C60315F196D3E";
            QueryAccess = "E20043F515B9473B31A0891E256DDF95";
            Endpoint = "https://freestyle-database.search.windows.net";
            Version = "2020-06-30";
        }

        private string RouteTemplate => Endpoint + "{0}" + $"?api-version={Version}";

        public async Task<WrestlingAggregatesModel> GetWrestlerMetaData(string wrestlerId)
        {
            await Task.Delay(250);

            return new WrestlingAggregatesModel
            {
                Losses = 1,
                Wins = 20,
                AverageDefensivePointsPerMatch = 25,
                AverageOffensivePointsPerMatch = 14,
                Pins = 12,
                Points = 240,
                Techs = 1000
            };
        }

        public async Task<bool> DoesIndexExist(CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}");

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            return response.IsSuccessStatusCode;
        }

        public async Task DeleteIndex(CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}");

            var request = new HttpRequestMessage(HttpMethod.Delete, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();
        }

        public async Task CreateIndex(CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, "/indexes");

            var request = new HttpRequestMessage(HttpMethod.Post, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            var payload = new
            {
                name = IndexName,
                fields = GetSchemeFromWrestlerModel(),
                corsOptions = new
                {
                    allowedOrigins = new[] { "*" }
                },
                suggesters = GetSuggesterFromWrestlerModel()
            };

            var payloadAsJson = JsonConvert
                .SerializeObject(payload);

            var payloadAsContent = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");

            request.Content = payloadAsContent;

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();
        }

        public async Task CreateDocuments(List<WrestlingDataModel> wrestlers, CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/index");

            var request = new HttpRequestMessage(HttpMethod.Post, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            if (wrestlers == null || wrestlers.Count == 0)
            {
                return;
            }

            var payload = new
            {
                value = GetDocumentsFromWrestlerModel(wrestlers)
            };

            var payloadAsJson = JsonConvert
                .SerializeObject(payload);

            var payloadAsContent = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");

            request.Content = payloadAsContent;

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();
        }

        public async Task<string> Search(HttpRequest httpRequest, CancellationToken cancellationToken = default)
        {
            var query = httpRequest.QueryString.Value.Replace('?', '&');
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs") + "&$count=true" + query;

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> AutoComplete(HttpRequest httpRequest, CancellationToken cancellationToken = default)
        {
            var query = httpRequest.QueryString.Value.Replace('?', '&');
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/autocomplete") + query;

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Suggest(HttpRequest httpRequest, CancellationToken cancellationToken = default)
        {
            var query = httpRequest.QueryString.Value.Replace('?', '&');
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/suggest") + query;

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Lookup(string documentId, CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/{documentId}");

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        private List<object> GetSuggesterFromWrestlerModel()
        {
            return new List<object>
            {
                new {
                    name = "ac",
                    searchMode = "analyzingInfixMatching",
                    sourceFields = new [] {
                        nameof(WrestlingDataModel.WrestlerName1),
                        nameof(WrestlingDataModel.WrestlerName2)
                    }
                }
            };
        }

        private List<object> GetSchemeFromWrestlerModel()
        {
            var type = typeof(WrestlingDataModel);
            var result = new List<object>();

            foreach (var prop in type.GetProperties())
            {
                if (!prop.CanRead)
                {
                    continue;
                }

                var isDate = prop.Name.Equals("date", StringComparison.OrdinalIgnoreCase);
                var isInt = prop.Name.Equals("WreslterName1Score", StringComparison.OrdinalIgnoreCase) || prop.Name.Equals("WreslterName2Score", StringComparison.OrdinalIgnoreCase);
                var isName = prop.Name.Equals("WreslterName1", StringComparison.OrdinalIgnoreCase) || prop.Name.Equals("WreslterName2", StringComparison.OrdinalIgnoreCase);

                result.Add(new
                {
                    name = prop.Name,
                    type = isDate
                        ? "Edm.DateTimeOffset"
                        : isInt
                            ? "Edm.Double"
                            : "Edm.String",
                    key = prop.Name.Equals("id", StringComparison.OrdinalIgnoreCase),
                    searchable = !isDate && !isInt,
                    filterable = true,
                    sortable = true,
                    facetable = true,
                    retrievable = true,
                    analyzer = "keyword"
                });
            }

            return result;
        }

        private List<WrestlingDataModel> GetDocumentsFromWrestlerModel(List<WrestlingDataModel> wrestlers)
        {
            return wrestlers;
        }
    }
}
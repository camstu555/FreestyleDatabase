using FreestyleDatabase.Shared.Extensions;
using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            if (wrestlers == null || wrestlers.Count == 0)
            {
                return;
            }

            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/index");

            var request = new HttpRequestMessage(HttpMethod.Post, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            var payload = new
            {
                value = wrestlers
            };

            var payloadAsJson = JsonConvert
                .SerializeObject(payload);

            var payloadAsContent = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");

            request.Content = payloadAsContent;

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();
        }

        public async Task UpdateDocument(List<WrestlingDataModel> wrestlers, CancellationToken cancellationToken = default)
        {
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/index");

            var request = new HttpRequestMessage(HttpMethod.Post, route);
            request.Headers.TryAddWithoutValidation("api-key", Access);

            var payload = new AzureRequest(AzureDocument.Create(wrestlers));

            var payloadAsJson = JsonConvert
                .SerializeObject(payload);

            var payloadAsContent = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");

            request.Content = payloadAsContent;

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();
        }

        public Task UpdateDocument(WrestlingDataModel wrestler, CancellationToken cancellationToken = default)
        {
            return UpdateDocument(new List<WrestlingDataModel> { wrestler }, cancellationToken);
        }

        public async Task<string> Search(Uri requestUri, CancellationToken cancellationToken = default)
        {
            var query = requestUri.Query.Replace('?', '&');
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs") + "&$count=true" + query;

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> Search<T>(Uri requestUri, CancellationToken cancellationToken = default)
        {
            var results = await Search(requestUri, cancellationToken);
            var parsed = JObject.Parse(results);
            var array = parsed["value"];

            return array.ToObject<T>();
        }

        public async Task<string> AutoComplete(Uri requestUri, CancellationToken cancellationToken = default)
        {
            var query = requestUri.Query.Replace('?', '&');
            var route = string.Format(RouteTemplate, $"/indexes/{IndexName}/docs/autocomplete") + query;

            var request = new HttpRequestMessage(HttpMethod.Get, route);
            request.Headers.TryAddWithoutValidation("api-key", QueryAccess);

            var response = await httpClient
                .SendAsync(request, cancellationToken);

            await response.CaptureFailedOperation();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Suggest(Uri requestUri, CancellationToken cancellationToken = default)
        {
            var query = requestUri.Query.Replace('?', '&');
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

                var isDate = prop.Name.Equals(nameof(WrestlingDataModel.Date), StringComparison.OrdinalIgnoreCase);

                var isDouble = prop.Name.Equals(nameof(WrestlingDataModel.WreslterName1Score), StringComparison.OrdinalIgnoreCase) ||
                            prop.Name.Equals(nameof(WrestlingDataModel.WreslterName2Score), StringComparison.OrdinalIgnoreCase);

                var isInt = prop.Name.Equals(nameof(WrestlingDataModel.MatchYear), StringComparison.OrdinalIgnoreCase)          ||
                            prop.Name.Equals(nameof(WrestlingDataModel.RecordNumber), StringComparison.OrdinalIgnoreCase);

                result.Add(new
                {
                    name = prop.Name,
                    type = isDate
                        ? "Edm.DateTimeOffset"
                        : isDouble
                            ? "Edm.Double"
                            : isInt 
                                ? "Edm.Int32"
                                : "Edm.String",
                    key = prop.Name.Equals("id", StringComparison.OrdinalIgnoreCase),
                    searchable = !isDate && !isInt && !isDouble,
                    filterable = true,
                    sortable = true,
                    facetable = true,
                    retrievable = true
                });
            }

            return result;
        }

        internal class AzureRequest
        {
            [JsonProperty(PropertyName = "value")]
            public List<AzureDocument> Value { get; private set; }

            public AzureRequest(AzureDocument document)
            {
                Value = new List<AzureDocument>() { document };
            }

            public AzureRequest(List<AzureDocument> documents)
            {
                Value = documents;
            }

            public AzureRequest()
            {
                Value = new List<AzureDocument>();
            }
        }

        internal class AzureDocument : WrestlingDataModel
        {
            [JsonProperty(PropertyName = "@search.action")]
            public string Action { get; set; } = "mergeOrUpload";

            public static AzureDocument Create(WrestlingDataModel dataModel)
            {
                return new AzureDocument
                {
                    Brackets = dataModel.Brackets,
                    Country1 = dataModel.Country1,
                    Country1Emoji = dataModel.Country1Emoji,
                    Date = dataModel.Date,
                    FullCountryName1 = dataModel.FullCountryName1,
                    FullCountryName2 = dataModel.FullCountryName2,
                    Country2 = dataModel.Country2,
                    Country2Emoji = dataModel.Country2Emoji,
                    Id = dataModel.Id,
                    Location = dataModel.Location,
                    RecordNumber = dataModel.RecordNumber,
                    Result = dataModel.Result,
                    Result2 = dataModel.Result2,
                    Round = dataModel.Round,
                    Score = dataModel.Score,
                    Title = dataModel.Title,
                    Venue = dataModel.Venue,
                    Video = dataModel.Video,
                    WeightClass = dataModel.WeightClass,
                    WreslterName1Score = dataModel.WreslterName1Score,
                    WreslterName2Score = dataModel.WreslterName2Score,
                    WrestlerId1 = dataModel.WrestlerId1,
                    WrestlerId2 = dataModel.WrestlerId2,
                    WrestlerImage1 = dataModel.WrestlerImage1,
                    WrestlerImage2 = dataModel.WrestlerImage2,
                    WrestlerName1 = dataModel.WrestlerName1,
                    WrestlerFirstName1 = dataModel.WrestlerFirstName1,
                    WrestlerLastName1 = dataModel.WrestlerLastName1,
                    WrestlerName2 = dataModel.
                    WrestlerFirstName2 = dataModel.WrestlerFirstName2,
                    WrestlerLastName2 = dataModel.WrestlerLastName2
                };
            }

            public static List<AzureDocument> Create(List<WrestlingDataModel> dataModel)
            {
                var results = new List<AzureDocument>();

                foreach (var item in dataModel)
                {
                    results.Add(Create(item));
                }

                return results;
            }
        }
    }
}
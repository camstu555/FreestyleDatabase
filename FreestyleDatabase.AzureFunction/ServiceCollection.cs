using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FreestyleDatabase.AzureFunction
{
    public static class ServiceCollection
    {
        public static BingImageSearchService BingImageSearchService = new BingImageSearchService(HttpClient, AzureSearchService, StorageAccountService);

        public static HttpClient HttpClient => new HttpClient();

        public static GoogleSheetService GoogleSheetService => new GoogleSheetService(HttpClient);

        public static WrestlingDataService WrestlingDataService => new WrestlingDataService(GoogleSheetService);

        public static AzureSearchService AzureSearchService => new AzureSearchService(HttpClient);

        public static WrestlerSearchService WrestlerSearchService => new WrestlerSearchService(HttpClient);

        public static StorageAccountService StorageAccountService => new StorageAccountService();

        public static Uri GetDisplayUrl(this HttpRequestData req)
        {
            var fieldInfo = req
                    .GetType()
                    .GetField("httpData", BindingFlags.NonPublic | BindingFlags.Instance);

            var fieldValue = (Microsoft.Azure.WebJobs.Script.Grpc.Messages.RpcHttp)fieldInfo
                .GetValue(req);

            return new Uri(fieldValue.Url);
        }

        public static RpcHttp GetRpcHttpData(this HttpRequestData req)
        {
            var fieldInfo = req
                    .GetType()
                    .GetField("httpData", BindingFlags.NonPublic | BindingFlags.Instance);

            return (RpcHttp)fieldInfo
                .GetValue(req);
        }

        public static HttpResponseData ToResponse(this Exception data, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => data.ToString().ToResponse(statusCode);

        public static HttpResponseData ToResponse(this string data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (string.IsNullOrEmpty(data))
            {
                statusCode = HttpStatusCode.NotFound;
            }

            var response = new HttpResponseData(statusCode, data);

            if (response.Headers == null)
            {
                response.Headers = new Dictionary<string, string>
                {
                    { "content-type", "application/json" }
                };
            }
            else
            {
                response.Headers.Add("content-type", "application/json");
            }

            return response;
        }

        public static HttpResponseData ToResponse(this List<WrestlingDataModel> wrestlingDataModels)
        {
            var data = JsonConvert.SerializeObject(wrestlingDataModels);

            return data.ToResponse();
        }

        public static HttpResponseData ToResponse(this DataAggregateModel dataAggregateModel)
        {
            var data = JsonConvert.SerializeObject(dataAggregateModel);

            return data.ToResponse();
        }

        public static HttpResponseData ToResponse(this WrestlingAggregatesModel wrestlingAggregatesModel)
        {
            var data = JsonConvert.SerializeObject(wrestlingAggregatesModel);

            return data.ToResponse();
        }

        public static DataAggregateModel ToDataModel(this List<WrestlingDataModel> wrestlingDataModels)
        {
            var result = new DataAggregateModel();

            var matches = wrestlingDataModels.Select(x => x.Id).Distinct().ToList();
            var forfeits = wrestlingDataModels.Where(x => x.IsForfeit).Select(x => x.Id).Distinct().ToList();
            var videos = wrestlingDataModels.Where(x => x.HasVideo).Select(x => x.Video).Distinct().ToList();
            var wrestler1s = wrestlingDataModels.Where(x => !string.IsNullOrEmpty(x.WrestlerName1)).Select(x => x.WrestlerName1).Distinct().ToList();
            var wrestler2s = wrestlingDataModels.Where(x => !string.IsNullOrEmpty(x.WrestlerName2)).Select(x => x.WrestlerName2).Distinct().ToList();

            var wrestlers = new List<string>();
            wrestlers.AddRange(wrestler1s);
            wrestlers.AddRange(wrestler2s);
            wrestlers = wrestlers.Distinct().ToList();

            var countries1s = wrestlingDataModels.Where(x => !string.IsNullOrEmpty(x.Country1)).Select(x => x.Country1).Distinct().ToList();
            var countries2s = wrestlingDataModels.Where(x => !string.IsNullOrEmpty(x.Country2)).Select(x => x.Country2).Distinct().ToList();

            var countries = new List<string>();
            countries.AddRange(countries1s);
            countries.AddRange(countries2s);
            countries = countries.Distinct().ToList();

            result.TotalWrestlers = wrestlers.Count;
            result.Wrestlers.AddRange(wrestlers);

            result.TotalMatches = matches.Count;
            result.Matches.AddRange(matches);

            result.MatchesWithForfeits.AddRange(forfeits);
            result.TotalMatchesWithForfeits = forfeits.Count;

            result.MatchesWithVideo.AddRange(videos);
            result.TotalMatchesWithVideo = videos.Count;

            result.Countries.AddRange(countries);
            result.TotalCountries = countries.Count;

            result.EarliestMatchDate = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.Date).FirstOrDefault();
            result.EarliestMatchId = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.Id).FirstOrDefault();
            result.EarliestMatchTitle = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.Title).FirstOrDefault();
            result.EarliestMatchWrestler1Image = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.WrestlerImage1).FirstOrDefault();
            result.EarliestMatchWrestler1Name = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.WrestlerName1).FirstOrDefault();
            result.EarliestMatchWrestler2Image = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.WrestlerImage2).FirstOrDefault();
            result.EarliestMatchWrestler2Name = wrestlingDataModels.OrderBy(x => x.Date).Select(x => x.WrestlerName2).FirstOrDefault();

            result.MostRecentMatchDate = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault();
            result.MostRecentMatchId = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.Id).FirstOrDefault();
            result.MostRecentMatchTitle = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.Title).FirstOrDefault();
            result.MostRecentMatchWrestler1Image = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.WrestlerImage1).FirstOrDefault();
            result.MostRecentMatchWrestler1Name = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.WrestlerName1).FirstOrDefault();
            result.MostRecentMatchWrestler2Image = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.WrestlerImage2).FirstOrDefault();
            result.MostRecentMatchWrestler2Name = wrestlingDataModels.OrderByDescending(x => x.Date).Select(x => x.WrestlerName2).FirstOrDefault();

            return result;
        }

        public static HttpResponseData ToResponse(this byte[] imageByes, string contentType)
        {
            var response = new HttpResponseData(HttpStatusCode.OK, System.Text.Encoding.ASCII.GetString(imageByes));

            if (response.Headers == null)
            {
                response.Headers = new Dictionary<string, string>
                {
                    { "content-type", contentType }
                };
            }
            else
            {
                response.Headers.Add("content-type", contentType);
            }

            return response;
        }
    }
}
using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public static HttpResponseData ToResponse(this WrestlingAggregatesModel wrestlingAggregatesModel)
        {
            var data = JsonConvert.SerializeObject(wrestlingAggregatesModel);

            return data.ToResponse();
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
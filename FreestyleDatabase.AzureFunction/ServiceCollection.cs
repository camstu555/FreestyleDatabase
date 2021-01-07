using FreestyleDatabase.Shared.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Script.Grpc.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace FreestyleDatabase.AzureFunction
{
    public static class ServiceCollection
    {
        public static BingImageSearchService BingImageSearchService = new BingImageSearchService(HttpClient);

        public static HttpClient HttpClient => new HttpClient();

        public static GoogleSheetService GoogleSheetService => new GoogleSheetService(HttpClient);

        public static WrestlingDataService WrestlingDataService => new WrestlingDataService(GoogleSheetService);

        public static AzureSearchService AzureSearchService => new AzureSearchService(HttpClient);

        public static WrestlerSearchService WrestlerSearchService => new WrestlerSearchService(HttpClient);

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
    }
}
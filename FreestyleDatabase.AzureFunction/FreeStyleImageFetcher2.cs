using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreeStyleImageFetcher2
    {
        [FunctionName(nameof(FreeStyleImageFetcher2))]
        public static async Task<byte[]> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            var wrestlerName = "NCAA Wrestling Wallpaper";

            if (req.Query.ContainsKey("name"))
            {
                wrestlerName = req.Query["name"];
            }

            var (imageResult, contentType) = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultBytes(wrestlerName);

            Console.WriteLine($"Found '{wrestlerName}' with byte count: {imageResult.Length}");

            return imageResult;
        }
    }
}

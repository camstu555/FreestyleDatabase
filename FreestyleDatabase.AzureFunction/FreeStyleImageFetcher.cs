using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreeStyleImageFetcher
    {
        [FunctionName(nameof(FreeStyleImageFetcher))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            Console.WriteLine("Attempting to look up a wrestler image...");

            try
            {
                var wrestlerName = "NCAA Wrestling Wallpaper";

                if (req.Query.ContainsKey("name"))
                {
                    wrestlerName = req.Query["name"];
                }

                var type = "json";

                if (req.Query.ContainsKey("type"))
                {
                    type = req.Query["type"];
                }


                Console.WriteLine($"Searching for '{wrestlerName}'");

                if (type.Equals("bytes", StringComparison.OrdinalIgnoreCase))
                {
                    var (imageResult, contentType) = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultBytes(wrestlerName);

                    Console.WriteLine($"Found '{wrestlerName}' with byte count: {imageResult.Length}");

                    return imageResult.ToResponse($"image/{contentType}");
                }
                else if (type.Equals("raw", StringComparison.OrdinalIgnoreCase))
                {
                    var imageResult = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultRaw(wrestlerName);

                    Console.WriteLine($"Found '{wrestlerName}' with url: {imageResult}");

                    return new HttpResponseData(System.Net.HttpStatusCode.OK, imageResult);
                }
                else
                {
                    var imageResult = await ServiceCollection.BingImageSearchService.GetWrestlerImageResult(wrestlerName);

                    Console.WriteLine($"Found '{wrestlerName}' with json: {imageResult}");

                    return imageResult.ToResponse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.Message);

                return ex.ToResponse();
            }
        }
    }
}
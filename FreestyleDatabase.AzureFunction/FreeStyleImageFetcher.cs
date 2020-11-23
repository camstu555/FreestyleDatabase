using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Attempting to look up a wrestler image...");

            var wrestlerName = req.Query["name"].Count > 0
                ? req.Query["name"].ToString()
                : null;

            var type = req.Query["type"].Count > 0
                ? req.Query["type"].ToString()
                : "json";

            log.LogInformation($"Searching for '{wrestlerName}'");

            try
            {
                if (type.Equals("bytes", StringComparison.OrdinalIgnoreCase))
                {
                    var (imageResult, contentType) = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultBytes(wrestlerName);

                    log.LogInformation($"Found '{wrestlerName}' with byte count: {imageResult.Length}");

                    return new FileContentResult(imageResult, $"image/{contentType}");
                }
                else if (type.Equals("raw", StringComparison.OrdinalIgnoreCase))
                {
                    var imageResult = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultRaw(wrestlerName);

                    log.LogInformation($"Found '{wrestlerName}' with url: {imageResult}");

                    return new ContentResult
                    {
                        Content = imageResult,
                        ContentType = "text/plain",
                        StatusCode = 200
                    };
                }
                else
                {
                    var imageResult = await ServiceCollection.BingImageSearchService.GetWrestlerImageResult(wrestlerName);

                    log.LogInformation($"Found '{wrestlerName}' with json: {imageResult}");

                    return new ContentResult
                    {
                        Content = imageResult,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }
            }
            catch (InvalidOperationException ex)
            {
                log.LogInformation("An error occured.");
                log.LogError(ex.Message);

                return new ContentResult
                {
                    Content = ex.Message,
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }
        }
    }
}
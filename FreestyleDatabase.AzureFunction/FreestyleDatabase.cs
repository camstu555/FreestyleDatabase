using FreestyleDatabase.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleDatabase
    {
        [FunctionName(nameof(FreestyleDatabase))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                if (req.Path.HasValue && req.Path.Value.EndsWith("search"))
                {
                    var searchResults = await ServiceCollection.AzureSearchService.Search(req);

                    return new ContentResult
                    {
                        Content = searchResults,
                        ContentType = "application/json",
                        StatusCode = 200
                    };
                }

                log.LogInformation("Attempting to fetch all wrestlers...");

                var wrestlers = await ServiceCollection
                    .WrestlingDataService
                    .GetWrestlerDataAsync();

                log.LogInformation($"Fetched {wrestlers.Count} wrestlers!");

                if (await ServiceCollection.AzureSearchService.DoesIndexExist())
                {
                    log.LogInformation("Attempting to tear down index...");
                    await ServiceCollection.AzureSearchService.DeleteIndex();
                    log.LogInformation("Index destroyed!");
                }

                log.LogInformation("Attempting to create new index...");

                await ServiceCollection.AzureSearchService.CreateIndex();

                log.LogInformation("Index created!");
                log.LogInformation("Attempting to populate index...");

                await ServiceCollection.AzureSearchService.CreateDocuments(wrestlers);

                log.LogInformation("Documents uploaded!");

                return new AcceptedResult();
            }
            catch (InvalidOperationException ex)
            {
                return new ContentResult
                {
                    Content = ex.Message,
                    ContentType = "application/json",
                    StatusCode = 400
                };
            }
        }
    }

    public static class ServiceCollection
    {
        public static HttpClient HttpClient => new HttpClient();

        public static GoogleSheetService GoogleSheetService => new GoogleSheetService(HttpClient);

        public static WrestlingDataService WrestlingDataService => new WrestlingDataService(GoogleSheetService);

        public static AzureSearchService AzureSearchService => new AzureSearchService(HttpClient);
    }
}
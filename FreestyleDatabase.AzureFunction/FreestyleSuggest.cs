using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleSuggest
    {
        [FunctionName(nameof(FreestyleSuggest))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Attempting to suggest a wrestlers...");

                var searchResults = await ServiceCollection.AzureSearchService.Suggest(req);

                return new ContentResult
                {
                    Content = searchResults,
                    ContentType = "application/json",
                    StatusCode = 200
                };
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
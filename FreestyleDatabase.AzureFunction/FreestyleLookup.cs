using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleLookup
    {
        [FunctionName(nameof(FreestyleLookup))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                log.LogInformation("Attempting to look up a wrestler...");

                var wrestlerId = req.Query["id"];
                var wrestlerName = req.Query["name"];
                var matchCount = req.Query["count"];

                if (string.IsNullOrEmpty(matchCount))
                {
                    matchCount = "10";
                }

                if (string.IsNullOrEmpty(wrestlerId) && string.IsNullOrEmpty(wrestlerName))
                {
                    return new NotFoundResult();
                }

                if (!string.IsNullOrEmpty(wrestlerName))
                {
                    var searchResults = await ServiceCollection.WrestlerSearchService.GetWrestlerDetailsByName(wrestlerName, Convert.ToInt32(matchCount));

                    return new ObjectResult(searchResults);
                }
                else
                {
                    var searchResults = await ServiceCollection.WrestlerSearchService.GetWrestlerDetails(wrestlerId, Convert.ToInt32(matchCount));

                    return new ObjectResult(searchResults);
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
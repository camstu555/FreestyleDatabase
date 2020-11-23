using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FreestyleDatabase.Shared.Extensions;

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

                var matchId = req.Query["id"].Count > 0
                    ? req.Query["id"].ToString() :
                    "0000000000000000";

                var searchResults = await ServiceCollection.AzureSearchService.Lookup(matchId);

                WrestlingAggregatesModel metaData = await ServiceCollection.AzureSearchService.GetWrestlerMetaData(matchId);

                searchResults = metaData.AppendToJson(searchResults);

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
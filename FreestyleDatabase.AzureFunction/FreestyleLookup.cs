using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleLookup
    {
        [FunctionName(nameof(FreestyleLookup))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            try
            {
                Console.WriteLine("Attempting to look up a wrestler...");

                var wrestlerId = string.Empty;

                if (req.Query.ContainsKey("id"))
                {
                    wrestlerId = req.Query["id"];
                }

                var wrestlerName = string.Empty;

                if (req.Query.ContainsKey("name"))
                {
                    wrestlerId = req.Query["name"];
                }

                var matchCount = "10";

                if (req.Query.ContainsKey("count"))
                {
                    matchCount = req.Query["count"];
                }

                if (string.IsNullOrEmpty(wrestlerId) && string.IsNullOrEmpty(wrestlerName))
                {
                    return string.Empty.ToResponse();
                }

                if (!string.IsNullOrEmpty(wrestlerName))
                {
                    var searchResults = await ServiceCollection
                        .WrestlerSearchService
                        .GetWrestlerDetailsByName(wrestlerName, Convert.ToInt32(matchCount));

                    return searchResults.ToResponse();
                }
                else
                {
                    var searchResults = await ServiceCollection
                        .WrestlerSearchService
                        .GetWrestlerDetails(wrestlerId, Convert.ToInt32(matchCount));

                    return searchResults.ToResponse();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.ToString());

                return ex.ToResponse();
            }
        }
    }
}
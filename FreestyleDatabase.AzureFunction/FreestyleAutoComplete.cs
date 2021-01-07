using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleAutoComplete
    {
        [FunctionName(nameof(FreestyleAutoComplete))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            try
            {
                Console.WriteLine("Attempting to autocomplete all wrestlers...");

                var url = req.GetDisplayUrl();

                Console.WriteLine(url);

                var searchResults = await ServiceCollection
                    .AzureSearchService
                    .AutoComplete(url);

                return searchResults.ToResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.ToString());

                return ex.ToResponse();
            }
        }
    }
}
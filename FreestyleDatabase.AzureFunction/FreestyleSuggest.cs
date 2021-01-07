using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleSuggest
    {
        [FunctionName(nameof(FreestyleSuggest))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            try
            {
                Console.WriteLine("Attempting to suggest a wrestlers...");

                var url = req.GetDisplayUrl();

                Console.WriteLine(url);

                var searchResults = await ServiceCollection
                    .AzureSearchService
                    .Suggest(url);

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
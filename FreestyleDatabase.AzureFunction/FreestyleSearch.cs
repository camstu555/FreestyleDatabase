using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleSearch
    {
        [FunctionName(nameof(FreestyleSearch))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {

            try
            {
                Console.WriteLine("Attempting to search all wrestlers...");

                var url = req.GetDisplayUrl();

                var searchResults = await ServiceCollection
                    .AzureSearchService
                    .Search(url);

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
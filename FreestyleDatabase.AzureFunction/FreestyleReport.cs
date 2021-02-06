using FreestyleDatabase.Shared.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleReport
    {
        [FunctionName(nameof(FreestyleReport))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            try
            {
                Console.WriteLine("Attempting to generate aggregate report...");

                var url = req.GetDisplayUrl();

                var searchResults = await ServiceCollection
                    .AzureSearchService
                    .GetAll<WrestlingDataModel>();

                var report = searchResults.ToDataModel();

                return report.ToResponse();
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
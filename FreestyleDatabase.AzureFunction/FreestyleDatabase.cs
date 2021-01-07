using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreestyleDatabase
    {
        [FunctionName(nameof(FreestyleDatabase))]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            try
            {
                Console.WriteLine("Attempting to fetch all wrestlers...");

                var wrestlers = await ServiceCollection
                    .WrestlingDataService
                    .GetWrestlerDataAsync();

                Console.WriteLine($"Fetched {wrestlers.Count} wrestlers!");

                if (await ServiceCollection.AzureSearchService.DoesIndexExist())
                {
                    Console.WriteLine("Attempting to tear down index...");
                    await ServiceCollection.AzureSearchService.DeleteIndex();
                    Console.WriteLine("Index destroyed!");
                }

                Console.WriteLine("Attempting to create new index...");

                await ServiceCollection
                    .AzureSearchService
                    .CreateIndex();

                Console.WriteLine("Index created!");
                Console.WriteLine("Attempting to populate index...");

                await ServiceCollection
                    .AzureSearchService
                    .CreateDocuments(wrestlers);

                return "\"Documents uploaded!\"".ToResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Console.WriteLine(ex.Message);

                return ex.ToResponse();
            }
        }
    }
}
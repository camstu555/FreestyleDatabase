using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace FreestyleDatabase.AzureFunction
{
    public static class FreeStyleImageFetcher2
    {
        [FunctionName(nameof(FreeStyleImageFetcher2))]
        public static async Task<byte[]> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
        {
            var wrestlerName = "NCAA Wrestling Wallpaper";

            if (req.Query.ContainsKey("name"))
            {
                wrestlerName = req.Query["name"];
            }

            var wrestlerId = string.Empty;

            if (req.Query.ContainsKey("id"))
            {
                wrestlerId = req.Query["id"];
            }

            var isStorage = req.Query.ContainsKey("storage");

            var hasThumbnail = req.Query.ContainsKey("type");

            if (isStorage)
            {
                var fileName = req.Query["storage"];

                var (bytes, _) = await ServiceCollection.StorageAccountService.GetFile(fileName);

                return bytes;
            }

            if (!hasThumbnail)
            {
                var (imageResult, _) = await ServiceCollection.BingImageSearchService.GetWrestlerImageResultBytes(wrestlerName, wrestlerId);

                Console.WriteLine($"Found '{wrestlerName}' with byte count: {imageResult.Length}");

                return imageResult;
            }
            else
            {
                var (imageResult, _) = await ServiceCollection.BingImageSearchService.GetWrestlerThumbailResultBytes(wrestlerName, wrestlerId);

                Console.WriteLine($"Found '{wrestlerName}' with byte count: {imageResult.Length}");

                return imageResult;
            }
        }
    }
}
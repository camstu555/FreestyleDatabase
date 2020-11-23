using FreestyleDatabase.Shared.Services;
using System.Net.Http;

namespace FreestyleDatabase.AzureFunction
{
    public static class ServiceCollection
    {
        public static BingImageSearchService BingImageSearchService = new BingImageSearchService(HttpClient);

        public static HttpClient HttpClient => new HttpClient();

        public static GoogleSheetService GoogleSheetService => new GoogleSheetService(HttpClient);

        public static WrestlingDataService WrestlingDataService => new WrestlingDataService(GoogleSheetService);

        public static AzureSearchService AzureSearchService => new AzureSearchService(HttpClient);
    }
}
using BlazorTransitionableRoute;
using FreestyleDatabase.Services;
using FreestyleDatabase.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreestyleDatabase
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var http = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };

            builder.Services.AddScoped(sp => http);

            builder.Services.AddScoped<GoogleSheetService>();
            builder.Services.AddScoped<BingImageSearchService>();
            builder.Services.AddScoped<AzureSearchService>();
            builder.Services.AddScoped<WrestlingDataService>();
            builder.Services.AddScoped<QueryParameterService>();
            builder.Services.AddScoped<WrestlerSearchService>();
            builder.Services.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();

            using var response = await http.GetAsync("https://freestyledb.azurewebsites.net/api/freestylereport");
            using var stream = await response.Content.ReadAsStreamAsync();

            builder.Configuration.AddJsonStream(stream);

            await builder.Build().RunAsync();
        }
    }
}
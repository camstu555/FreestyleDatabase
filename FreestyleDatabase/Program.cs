using FreestyleDatabase.Services;
using FreestyleDatabase.Shared.Services;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorTransitionableRoute;

namespace FreestyleDatabase
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<GoogleSheetService>();
            builder.Services.AddScoped<BingImageSearchService>();
            builder.Services.AddScoped<AzureSearchService>();
            builder.Services.AddScoped<WrestlingDataService>();
            builder.Services.AddScoped<QueryParameterService>();
            builder.Services.AddScoped<WrestlerSearchService>(); 
            builder.Services.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();


            await builder.Build().RunAsync();
        }
    }
}
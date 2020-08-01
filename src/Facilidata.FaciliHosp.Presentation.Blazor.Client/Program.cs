using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Facilidata.FaciliHosp.Presentation.Blazor.Client.Services;
using Facilidata.FaciliHosp.Presentation.Blazor.Client.HttpClientHandlers;
using Microsoft.Extensions.Caching.Memory;
using Facilidata.FaciliHosp.Presentation.Blazor.Client.Models;

namespace Facilidata.FaciliHosp.Presentation.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddMemoryCache();
            var memoryCache = builder.Services.BuildServiceProvider().GetService<IMemoryCache>();

            builder.Services.AddScoped((sp)=>
            {
                return new HttpClient() { BaseAddress = new Uri("http://localhost:61111/api/") };
                //var memoryCache = sp.GetService<IMemoryCache>();
                //bool existTokenContainer = memoryCache.TryGetValue("TokenContainer", out TokenResponse tokenResponse);
                //Console.WriteLine(existTokenContainer);
                //var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:61111/api/") };
                //if (existTokenContainer && !string.IsNullOrEmpty(tokenResponse?.Token))
                //{
                //    Console.WriteLine("Send Auth");
                //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", tokenResponse.Token);
                //}
                //return httpClient;
            });
            builder.Services.AddTransient<WsService>();

            await builder.Build().RunAsync();
        }
    }
}

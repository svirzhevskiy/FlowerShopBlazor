using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlowerShopBlazor.Application;
using FlowerShopBlazor.Services;
using FlowerShopBlazor.Services.ApiService;

namespace FlowerShopBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddScoped<IApiService, ApiService>();
            
            builder.Services.AddScoped<IToastService, ToastService>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            await builder.Build().RunAsync();
        }
    }
}

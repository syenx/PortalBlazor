using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontSPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var baseAdress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAdress) });
            builder.Services.AddMsalAuthentication(opt =>
            {
                builder.Configuration.Bind("AzureAd", opt.ProviderOptions.Authentication);
                opt.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/openid");
                opt.ProviderOptions.LoginMode = "redirect";
            });
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}

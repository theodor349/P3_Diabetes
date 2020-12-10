using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PWA.Network;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace PWA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var client = new HttpClient { BaseAddress = new Uri("https://localhost:5003") };
            builder.Services.AddScoped(sp => client);
            INetworkHelper network = new NetworkHelperLocal(client, "https://localhost:5003");
            builder.Services.AddScoped(sp => network);

            await builder.Build().RunAsync();
        }
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Persons
{
    internal class Program
    {     
        private static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            var host = builder.Build();
            await host.RunAsync();           
        }
    }
}
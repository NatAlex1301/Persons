
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using SendGrid.Helpers.Errors.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Persons
{
    internal class Program
    {     
        private static async Task Main(string[] args)
        {

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            var host = builder.Build();
            host.RunAsync();

            Console.WriteLine("Нажмите C для завершения");

            while (true)
            {
                var keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.C)
                {
                    await host.StopAsync();
                    break;
                }
                else
                {
                    Console.WriteLine($"Вы ввели{keyPressed}");
                }


            }


        }
    }
}
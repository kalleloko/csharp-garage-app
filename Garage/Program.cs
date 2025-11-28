using GarageApp.Interfaces;
using GarageApp.Models;
using GarageApp.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GarageApp;

internal class Program
{

    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<IGarage<IVehicle>, Garage<IVehicle>>();
                services.AddSingleton<IUI, ConsoleUI>();
                services.AddSingleton<ISearchParser, SearchParser>();
                services.AddSingleton<IGarageHandler<IVehicle>, GarageHandler<IVehicle>>();
                services.AddSingleton<GarageManager>();
            })
            .UseConsoleLifetime()
            .Build();

        host.Services.GetRequiredService<GarageManager>().Run();
    }
}

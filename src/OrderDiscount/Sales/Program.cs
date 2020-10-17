using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales";
            var endpointInstance = await Endpoint.Start(new ConfigurationFactory().CreateConfiguration());

            Console.WriteLine($"{Console.Title} started.");
            Console.WriteLine($"Press any key to simulate publishing an OrderPlaced event.");
            Console.ReadLine();

            Console.WriteLine($"OrderPlaced published.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
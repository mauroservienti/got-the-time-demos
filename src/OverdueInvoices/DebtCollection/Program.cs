using System;
using System.Threading.Tasks;
using NServiceBus;

namespace DebtCollection
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "DebtCollection";
            var endpointInstance = await Endpoint.Start(new ConfigurationFactory().CreateConfiguration());

            Console.WriteLine($"{Console.Title} started. Press any key to stop.");
            Console.ReadLine();
            
            await endpointInstance.Stop();
        }
    }
}
using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Finance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Finance";

            var config = new FinanceEndpointConfiguration();

            var endpointInstance = await Endpoint.Start(config);

            Console.WriteLine($"{Console.Title} started. Press any key to stop.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}

using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Configuration.AdvancedExtensibility;

namespace DebtCollection
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointConfiguration = new ConfigurationFactory().CreateConfiguration();
            var endpointName = endpointConfiguration.GetSettings().EndpointName();
            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            Console.Title = endpointName;
            Console.WriteLine($"{endpointName} started. Press any key to stop.");
            Console.ReadLine();
            
            await endpointInstance.Stop();
        }
    }
}
using System;
using System.Threading.Tasks;
using NServiceBus;
using OverdueInvoices;

namespace Finance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Finance";
            var endpointInstance = await Endpoint.Start(ConfigurationFactory.CreateConfiguration());

            Console.WriteLine($"{Console.Title} started.");
            Console.WriteLine($"Press any key to simulate publishing an InvoiceIssued event.");
            Console.WriteLine($"The issued invoice will have a DueDate of DateTime.Now + 30 seconds so to not wait too much for the payment check ;-).");
            Console.WriteLine($"It's obvious that it cannot be an Italian customer :-P.");
            Console.ReadLine();

            await endpointInstance.Publish(new InvoiceIssuedEvent()
            {
                InvoiceNumber = 123,
                DueDate = DateTime.Now.AddSeconds(30),
                CustomerCountry = "not Italy"
            });

            Console.WriteLine($"InvoiceIssued published.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}

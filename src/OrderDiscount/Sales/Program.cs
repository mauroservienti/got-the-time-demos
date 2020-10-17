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
            Console.WriteLine("Press any key to simulate publishing 3 OrderPlaced events.");
            Console.WriteLine("Discount threshold is 300$, MonthlyRunningTotal deduction time is set to 1 minute for this demo.");
            Console.WriteLine("First order total amount will be 150$, no discount will be applied");
            Console.WriteLine("Second order total amount will be 250$, no discount will be applied");
            Console.WriteLine("Third order total amount will be 100$, 10% discount will be applied");
            Console.ReadLine();

            await endpointInstance.Publish(new OrderPlacedEvent()
            {
                CustomerId = 123,
                OrderId = 789,
                OrderTotalAmount = 150
            });
            
            await endpointInstance.Publish(new OrderPlacedEvent()
            {
                CustomerId = 123,
                OrderId = 70,
                OrderTotalAmount = 250
            });
            
            await endpointInstance.Publish(new OrderPlacedEvent()
            {
                CustomerId = 123,
                OrderId = 456,
                OrderTotalAmount = 100
            });

            Console.WriteLine($"OrderPlaced events published.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
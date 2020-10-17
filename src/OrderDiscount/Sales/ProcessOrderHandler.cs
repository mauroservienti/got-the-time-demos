using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Sales
{
    class ProcessOrderHandler : IHandleMessages<ProcessOrder>
    {
        public Task Handle(ProcessOrder message, IMessageHandlerContext context)
        {
            Console.WriteLine($"ProcessOrderHandler - ProcessOrder, for order {message.OrderId}, message received.");
            Console.WriteLine(message.Discount > 0
                ? $"ProcessOrderHandler - Applied discount: {message.Discount}%."
                : "ProcessOrderHandler - No discount will be applied.");
            
            return Task.CompletedTask;
        }
    }
}
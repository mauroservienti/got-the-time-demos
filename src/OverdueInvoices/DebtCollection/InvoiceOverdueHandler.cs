using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace DebtCollection
{
    public class InvoiceOverdueHandler : IHandleMessages<InvoiceOverdue>
    {
        public Task Handle(InvoiceOverdue message, IMessageHandlerContext context)
        {
            Console.WriteLine($"InvoiceOverdueHandler - invoice {message.InvoiceNumber} is overdue. Let's get in touch with the customer.");
            return Task.CompletedTask;
        }
    }
}
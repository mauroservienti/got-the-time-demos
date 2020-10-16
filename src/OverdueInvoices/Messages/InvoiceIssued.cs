using System;
using NServiceBus;

namespace Messages
{
    public interface InvoiceIssued : IEvent
    {
        int InvoiceNumber { get; }
        DateTime DueDate { get; }
        string CustomerCountry { get; }
    }
}
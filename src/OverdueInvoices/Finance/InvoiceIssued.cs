using System;
using NServiceBus;

namespace Finance
{
    public interface InvoiceIssued : IEvent
    {
        int InvoiceNumber { get; }
        DateTime DueDate { get; }
        string CustomerCountry { get; }
    }
}
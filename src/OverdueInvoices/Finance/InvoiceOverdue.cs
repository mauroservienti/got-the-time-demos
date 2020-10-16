using NServiceBus;

namespace Finance
{
    public interface InvoiceOverdue : IEvent
    {
        int InvoiceNumber { get; }
    }
}
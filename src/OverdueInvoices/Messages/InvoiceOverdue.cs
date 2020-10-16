using NServiceBus;

namespace Messages
{
    public interface InvoiceOverdue : IEvent
    {
        int InvoiceNumber { get; }
    }
}
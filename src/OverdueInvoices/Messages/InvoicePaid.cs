using NServiceBus;

namespace Messages
{
    public interface InvoicePaid : IEvent
    {
        int InvoiceNumber { get; }
    }
}
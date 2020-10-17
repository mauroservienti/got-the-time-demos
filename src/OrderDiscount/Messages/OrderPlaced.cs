using NServiceBus;

namespace Messages
{
    public interface OrderPlaced : IEvent
    {
        int CustomerId { get; }
        int OrderId { get; }
        decimal OrderTotalAmount { get; }
    }
}
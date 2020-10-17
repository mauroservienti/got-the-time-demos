using System;

namespace Messages
{
    public interface OrderPlaced
    {
        int OrderId { get; }
        decimal OrderTotalAmount { get; }
    }
}
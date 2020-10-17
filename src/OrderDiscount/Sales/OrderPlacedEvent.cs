using Messages;

namespace Sales
{
    public class OrderPlacedEvent : OrderPlaced
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal OrderTotalAmount { get; set; }
    }
}
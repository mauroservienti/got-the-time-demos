using Messages;

namespace Sales
{
    class OrderPlacedEvent : OrderPlaced
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal OrderTotalAmount { get; set; }
    }
}
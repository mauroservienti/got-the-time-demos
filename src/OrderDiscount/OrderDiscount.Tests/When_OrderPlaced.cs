using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using NUnit.Framework;
using Sales;

namespace OrderDiscount.Tests
{
    public class When_OrderPlaced
    {
        [Test]
        public async Task DeductFromRunningTotal_timeout_is_requested()
        {
            var saga = new OrderDiscountPolicy()
            {
                Data = new OrderDiscountPolicy.OrderDiscountData()
                {
                    CustomerId = 123 
                }
            };
            var context = new TestableMessageHandlerContext();

            int expectedOrderTotalAmount = 100;
            var orderPlaced = new OrderPlacedEvent()
            {
                CustomerId = 123,
                OrderId = 789,
                OrderTotalAmount = expectedOrderTotalAmount
            };

            await saga.Handle(orderPlaced, context);

            var deductFromRunningTotalTimeout = context.SentMessages.SingleOrDefault(m => m.Message.GetType() == typeof(OrderDiscountPolicy.DeductFromRunningTotal));
            
            Assert.That(deductFromRunningTotalTimeout, Is.Not.Null);
            Assert.That(deductFromRunningTotalTimeout.Message<OrderDiscountPolicy.DeductFromRunningTotal>().OrderTotalAmount, Is.EqualTo(expectedOrderTotalAmount));
        }
    }
}
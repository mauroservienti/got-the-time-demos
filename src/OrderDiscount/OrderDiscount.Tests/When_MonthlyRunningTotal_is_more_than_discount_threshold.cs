using System.Linq;
using System.Threading.Tasks;
using NServiceBus.Testing;
using NUnit.Framework;
using Sales;

namespace OrderDiscount.Tests
{
    public class When_MonthlyRunningTotal_is_more_than_discount_threshold
    {
        [Test]
        public async Task Discount_is_applied()
        {
            var saga = new OrderDiscountPolicy()
            {
                Data = new OrderDiscountPolicy.OrderDiscountData()
                {
                    CustomerId = 123,
                    MonthlyRunningTotal = 350
                }
            };
            var context = new TestableMessageHandlerContext();
            
            var orderPlaced = new OrderPlacedEvent()
            {
                CustomerId = 123,
                OrderId = 789,
                OrderTotalAmount = 100
            };

            await saga.Handle(orderPlaced, context);

            var processOrderMessage = context.SentMessages.SingleOrDefault(m => m.Message.GetType() == typeof(ProcessOrder));
            
            Assert.That(processOrderMessage, Is.Not.Null);
            Assert.That(processOrderMessage.Message<ProcessOrder>().Discount, Is.EqualTo(10));
        }
    }
}
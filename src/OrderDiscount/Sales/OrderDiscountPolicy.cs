using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Sales
{
    class OrderDiscountPolicy :
        Saga<OrderDiscountPolicy.OrderDiscountData>,
        IAmStartedByMessages<OrderPlaced>,
        IHandleTimeouts<OrderDiscountPolicy.DeductFromRunningTotal>
    {
        internal class OrderDiscountData : ContainSagaData
        {
            public int CustomerId { get; set; }
            public decimal MonthlyRunningTotal { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderDiscountData> mapper)
        {
            mapper.MapSaga(d => d.CustomerId).ToMessage<OrderPlaced>(m => m.CustomerId);
        }

        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Received OrderPlaced event for order {message.OrderId} from customer {message.CustomerId}, total amount {message.OrderTotalAmount}$.");
            Console.WriteLine($"Customer {Data.CustomerId} MonthlyRunningTotal is {Data.MonthlyRunningTotal}$.");
            
            var discount = Data.MonthlyRunningTotal > 300 ? 10 : 0;
            Console.WriteLine($"Calculated discount for order {message.OrderId} is {discount}%.");
            
            Data.MonthlyRunningTotal += message.OrderTotalAmount;
            await context.SendLocal(new ProcessOrder() 
            {
                OrderId = message.OrderId,
                Discount = discount    
            });
            Console.WriteLine($"ProcessOrder for order {message.OrderId} command sent.");
            
            var delay = DateTime.Now.AddMinutes(1);
            await RequestTimeout(context, delay, new DeductFromRunningTotal()
            {
                OrderTotalAmount = message.OrderTotalAmount
            });
            Console.WriteLine($"Timeout for order {message.OrderId} requested.");
        }

        public Task Timeout(DeductFromRunningTotal state, IMessageHandlerContext context)
        {
            Console.WriteLine($"Going to deduct from MonthlyRunningTotal of {Data.MonthlyRunningTotal}$ the amount of {state.OrderTotalAmount}$ for customer {Data.CustomerId}.");
            Data.MonthlyRunningTotal -= state.OrderTotalAmount;
            if (Data.MonthlyRunningTotal < 0)
            {
                Data.MonthlyRunningTotal = 0;
            }
            Console.WriteLine($"Customer {Data.CustomerId} new MonthlyRunningTotal is {Data.MonthlyRunningTotal}$.");
            
            return Task.CompletedTask;
        }

        internal class DeductFromRunningTotal
        {
            public decimal OrderTotalAmount { get; set; }
        }
    }
}
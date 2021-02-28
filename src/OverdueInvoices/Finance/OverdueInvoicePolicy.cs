using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Finance
{
    class OverdueInvoicePolicy :
        Saga<OverdueInvoicePolicy.OverdueInvoiceData>,
        IAmStartedByMessages<InvoiceIssued>,
        IHandleMessages<InvoicePaid>,
        IHandleTimeouts<CheckPayment>
    {
        internal class OverdueInvoiceData : ContainSagaData
        {
            public int InvoiceNumber { get; set; }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OverdueInvoiceData> mapper)
        {
            mapper.MapSaga(d => d.InvoiceNumber)
                .ToMessage<InvoiceIssued>(m => m.InvoiceNumber)
                .ToMessage<InvoicePaid>(m => m.InvoiceNumber);
        }

        public async Task Handle(InvoiceIssued message, IMessageHandlerContext context)
        {
            Console.WriteLine($"OverdueInvoicePolicy - InvoiceIssued: {message.InvoiceNumber}, DueDate {message.DueDate}");

            Data.InvoiceNumber = message.InvoiceNumber;
            var dueDate = message.DueDate;
            if (message.CustomerCountry == "Italy")
            {
                dueDate = dueDate.AddDays(20);
            }

            await RequestTimeout<CheckPayment>(context, dueDate);
            Console.WriteLine($"OverdueInvoicePolicy - CheckPayment scheduled for: {dueDate}");
        }

        public Task Handle(InvoicePaid message, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.CompletedTask;
        }

        public async Task Timeout(CheckPayment state, IMessageHandlerContext context)
        {
            //If the timeout is received it means we never received the InvoicePaid message, by definition this invoice is overdue.
            Console.WriteLine($"OverdueInvoicePolicy - Invoice {Data.InvoiceNumber} is overdue, publishing InvoiceOverdue event.");
            await context.Publish(new InvoiceOverdueEvent() {InvoiceNumber = Data.InvoiceNumber});
            MarkAsComplete();
        }
    }
}
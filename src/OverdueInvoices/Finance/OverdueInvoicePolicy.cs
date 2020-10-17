﻿using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Finance
{
    class OverdueInvoicePolicy :
        Saga<OverdueInvoicePolicy.OverdueInvoiceData>,
        IAmStartedByMessages<InvoiceIssued>,
        IHandleTimeouts<CheckPayment>
    {
        private readonly IInvoiceService _invoiceService;

        internal class OverdueInvoiceData : ContainSagaData
        {
            public int InvoiceNumber { get; set; }
        }

        public OverdueInvoicePolicy(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OverdueInvoiceData> mapper)
        {
            mapper.MapSaga(d => d.InvoiceNumber).ToMessage<InvoiceIssued>(m => m.InvoiceNumber);
        }

        public async Task Handle(InvoiceIssued message, IMessageHandlerContext context)
        {
            Data.InvoiceNumber = message.InvoiceNumber;
            var dueDate = message.DueDate;
            if(message.CustomerCountry == "Italy")
            {
                dueDate = dueDate.AddDays(20);
            }
            await RequestTimeout<CheckPayment>(context, dueDate);
        }

        public async Task Timeout(CheckPayment state, IMessageHandlerContext context)
        {
            var invoiceNumber = Data.InvoiceNumber;
            var isInvoicePaid = _invoiceService.IsInvoicePaid(invoiceNumber);
            if(!isInvoicePaid)
            {
                await context.Publish(new InvoiceOverdueEvent()
                {
                    InvoiceNumber = invoiceNumber
                });
            }

            MarkAsComplete();
        }
    }
}
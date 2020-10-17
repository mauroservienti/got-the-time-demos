using Messages;
using NServiceBus;
using NServiceBus.AcceptanceTesting;
using NServiceBus.DelayedDelivery;
using NServiceBus.IntegrationTesting;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Finance;

namespace OverdueInvoices.IntegrationTests
{
    public class When_an_issued_invoice_is_not_paid
    {
        [Test]
        public async Task OverdueInvoicePolicy_is_completed_and_InvoiceOverdue_is_processed()
        {
            var context = await Scenario.Define<IntegrationScenarioContext>(ctx =>
                {
                    ctx.RegisterTimeoutRescheduleRule<CheckPayment>((msg, delay) => new DoNotDeliverBefore(DateTime.UtcNow.AddSeconds(2)));
                })
                .WithEndpoint<FinanceEndpoint>(behavior =>
                {
                    behavior.When(session => session.Publish(new TestInvoiceIssuedEvent()
                    {
                        InvoiceNumber = 123,
                        DueDate = DateTime.Now.AddMonths(1),
                        CustomerCountry = "not italy"
                    }));
                })
                .Done(ctx => ctx.HandlerWasInvoked<InvoiceOverdueHandler>() || ctx.HasFailedMessages())
                .Run();

            Assert.True(context.SagaWasCompleted<OverdueInvoicePolicy>());
            Assert.True(context.MessageWasProcessedByHandler<InvoiceOverdue, InvoiceOverdueHandler>());
            Assert.False(context.HasFailedMessages());
            Assert.False(context.HasHandlingErrors());
        }

        class FinanceEndpoint : EndpointConfigurationBuilder
        {
            public FinanceEndpoint()
            {
                EndpointSetup(new FinanceEndpointTemplate(new NeverPaidInvoiceService()), (endpointConfiguration, descriptor) => { });
            }
        }
    }
}
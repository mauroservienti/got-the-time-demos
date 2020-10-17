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
    public class When_an_invoice_is_issued
    {
        [Test]
        public async Task OverdueInvoicePolicy_is_completed_as_expected()
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
                .Done(ctx => ctx.SagaWasCompleted<OverdueInvoicePolicy>() || ctx.HasFailedMessages())
                .Run();

            Assert.True(context.MessageWasProcessedBySaga<InvoiceIssued, OverdueInvoicePolicy>());
            Assert.True(context.MessageWasProcessedBySaga<CheckPayment, OverdueInvoicePolicy>());
            Assert.False(context.HasFailedMessages());
            Assert.False(context.HasHandlingErrors());
        }

        class FinanceEndpoint : EndpointConfigurationBuilder
        {
            public FinanceEndpoint()
            {
                EndpointSetup(new FinanceEndpointTemplate(new AlwaysPaidInvoiceService()), (endpointConfiguration, descriptor) => { });
            }
        }
    }
}
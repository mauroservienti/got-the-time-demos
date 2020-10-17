using System;
using System.Threading.Tasks;
using Finance;
using NServiceBus;
using NServiceBus.AcceptanceTesting.Support;
using NServiceBus.IntegrationTesting;

namespace OverdueInvoices.IntegrationTests
{
    class FinanceEndpointTemplate : EndpointTemplate
    {
        private readonly IInvoiceService _invoiceService;

        public FinanceEndpointTemplate(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        
        protected override Task<EndpointConfiguration> OnGetConfiguration(RunDescriptor runDescriptor,
            EndpointCustomizationConfiguration endpointCustomizationConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization)
        {
            var factory = new ConfigurationFactory
            {
                InvoiceServiceFactory = () => _invoiceService
            };
            return Task.FromResult(factory.CreateConfiguration());
        }
    }
}
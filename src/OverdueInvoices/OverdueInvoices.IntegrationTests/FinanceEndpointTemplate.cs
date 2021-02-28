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
        protected override Task<EndpointConfiguration> OnGetConfiguration(RunDescriptor runDescriptor,
            EndpointCustomizationConfiguration endpointCustomizationConfiguration, Action<EndpointConfiguration> configurationBuilderCustomization)
        {
            return Task.FromResult(ConfigurationFactory.CreateConfiguration());
        }
    }
}
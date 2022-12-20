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
            EndpointCustomizationConfiguration endpointCustomizationConfiguration, Func<EndpointConfiguration, Task> configurationBuilderCustomization)
        {
            return Task.FromResult(ConfigurationFactory.CreateConfiguration());
        }
    }
}
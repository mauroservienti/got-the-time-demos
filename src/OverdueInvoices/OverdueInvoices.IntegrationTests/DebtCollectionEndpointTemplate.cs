using System;
using System.Threading.Tasks;
using DebtCollection;
using NServiceBus;
using NServiceBus.AcceptanceTesting.Support;
using NServiceBus.IntegrationTesting;

namespace OverdueInvoices.IntegrationTests
{
    class DebtCollectionEndpointTemplate : EndpointTemplate
    {
        protected override Task<EndpointConfiguration> OnGetConfiguration(RunDescriptor runDescriptor,
            EndpointCustomizationConfiguration endpointCustomizationConfiguration, Func<EndpointConfiguration, Task> configurationBuilderCustomization)
        {
            var factory = new ConfigurationFactory();
            return Task.FromResult(factory.CreateConfiguration());
        }
    }
}
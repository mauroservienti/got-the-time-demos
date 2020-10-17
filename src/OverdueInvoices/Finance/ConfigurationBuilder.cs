using System;
using NServiceBus;

namespace Finance
{
    class ConfigurationFactory
    {
        internal Func<IInvoiceService> InvoiceServiceFactory = () => new InvoiceService();
        
        public EndpointConfiguration CreateConfiguration()
        {
            var config = new EndpointConfiguration("Finance");
            
            config.UseTransport<LearningTransport>();
            config.UsePersistence<LearningPersistence>();
            config.SendFailedMessagesTo("error");

            config.RegisterComponents(components =>
            {
                components.ConfigureComponent<IInvoiceService>(
                    InvoiceServiceFactory,
                    DependencyLifecycle.SingleInstance);
            });

            return config;
        }
    }
}
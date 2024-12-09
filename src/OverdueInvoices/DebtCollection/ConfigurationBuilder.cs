

using NServiceBus;

namespace DebtCollection
{
    class ConfigurationFactory
    {
        public EndpointConfiguration CreateConfiguration()
        {
            var config = new EndpointConfiguration("DebtCollection");
            
            config.UseSerialization<SystemJsonSerializer>();
            config.UseTransport<LearningTransport>();
            config.SendFailedMessagesTo("error");
            
            var scanner = config.AssemblyScanner();
            scanner.IncludeOnlyThisAssemblyAndReferences();

            return config;
        }
    }
}
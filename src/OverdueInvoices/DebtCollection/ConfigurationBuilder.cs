

using NServiceBus;

namespace DebtCollection
{
    class ConfigurationFactory
    {
        public EndpointConfiguration CreateConfiguration()
        {
            var config = new EndpointConfiguration("DebtCollection");
            
            config.UseTransport<LearningTransport>();
            config.SendFailedMessagesTo("error");
            
            var scanner = config.AssemblyScanner();
            scanner.IncludeOnly("DebtCollection.dll", "Messages.dll");

            return config;
        }
    }
}
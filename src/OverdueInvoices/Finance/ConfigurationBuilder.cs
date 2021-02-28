using System;
using NServiceBus;

namespace Finance
{
    static class ConfigurationFactory
    {
        public static EndpointConfiguration CreateConfiguration()
        {
            var config = new EndpointConfiguration("Finance");

            config.UseTransport<LearningTransport>();
            config.UsePersistence<LearningPersistence>();
            config.SendFailedMessagesTo("error");

            var scanner = config.AssemblyScanner();
            scanner.IncludeOnlyThisAssemblyAndReferences();

            return config;
        }
    }
}
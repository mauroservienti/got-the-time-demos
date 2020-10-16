using NServiceBus;

namespace Finance
{
    class FinanceEndpointConfiguration : EndpointConfiguration
    {
        public FinanceEndpointConfiguration() : base("Finance")
        {
            this.UseTransport<LearningTransport>();
            this.UsePersistence<LearningPersistence>();
            this.SendFailedMessagesTo("error");
        }
    }
}
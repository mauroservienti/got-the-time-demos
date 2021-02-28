using System.Linq;
using NServiceBus.IntegrationTesting;

namespace OverdueInvoices.IntegrationTests
{
    public static class IntegrationScenarioContextExtensions
    {
        public static bool EventWasPublished<TEvent>(this IntegrationScenarioContext context)
        {
            var operation = context.OutgoingMessageOperations.SingleOrDefault(o =>
                o is PublishOperation
                && typeof(TEvent).IsAssignableFrom(o.MessageType));

            return operation != null;
        }

        public static bool MessageWasSent<TMessage>(this IntegrationScenarioContext context)
        {
            var operation = context.OutgoingMessageOperations.SingleOrDefault(o =>
                o is SendOperation
                && typeof(TMessage).IsAssignableFrom(o.MessageType));

            return operation != null;
        }

        public static bool ReplyWasSent<TReply>(this IntegrationScenarioContext context)
        {
            var operation = context.OutgoingMessageOperations.SingleOrDefault(o =>
                o is ReplyOperation
                && typeof(TReply).IsAssignableFrom(o.MessageType));

            return operation != null;
        }

        public static bool TimeoutWasScheduled<TTimeout>(this IntegrationScenarioContext context)
        {
            var operation = context.OutgoingMessageOperations.SingleOrDefault(o =>
                o is RequestTimeoutOperation
                && typeof(TTimeout).IsAssignableFrom(o.MessageType));

            return operation != null;
        }
    }
}
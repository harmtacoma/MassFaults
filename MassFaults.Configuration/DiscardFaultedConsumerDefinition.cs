using MassTransit;

namespace MassFaults.Configuration
{
    public class DiscardFaultedConsumerDefinition<T> : ConsumerDefinition<T> where T : class, IConsumer
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<T> consumerConfigurator)
        {
            endpointConfigurator.DiscardFaultedMessages();
        }
    }
}

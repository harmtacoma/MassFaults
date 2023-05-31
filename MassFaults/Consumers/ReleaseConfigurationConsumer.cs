using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.Consumers
{
    public class ReleaseConfigurationConsumer : IConsumer<ReleaseConfiguration>
    {
        public Task Consume(ConsumeContext<ReleaseConfiguration> context)
        {
            context.Publish(new Released()
            {
                ConfigurationId = context.Message.ConfigurationId
            });

            return Task.CompletedTask;
        }
    }
}

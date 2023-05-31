using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.Consumers
{
    public class ImportConfigurationConsumer : IConsumer<ImportConfiguration>
    {
        public Task Consume(ConsumeContext<ImportConfiguration> context)
        {
            context.Publish(new ConfigurationImported()
            {
                ConfigurationId = context.Message.ConfigurationId
            });

            return Task.CompletedTask;
        }
    }
}

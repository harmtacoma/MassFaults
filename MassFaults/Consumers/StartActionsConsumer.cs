using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.Consumers
{
    public class StartActionsConsumer : IConsumer<StartActions>
    {
        public Task Consume(ConsumeContext<StartActions> context)
        {
            context.Publish(new ActionsDone()
            {
                ConfigurationId = context.Message.ConfigurationId
            });

            return Task.CompletedTask;
        }
    }
}

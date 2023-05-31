using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.StateMachine
{
    public class ConfigurationStateMachine : MassTransitStateMachine<ConfigurationState>
    {
        public ConfigurationStateMachine()
        {
            Event(() => ConfigurationSubmitted, x => x.CorrelateById(context => context.Message.ConfigurationId));
            Event(() => ConfigurationImported, x => x.CorrelateById(context => context.Message.ConfigurationId));
            Event(() => ActionsDone, x => x.CorrelateById(context => context.Message.ConfigurationId));
            Event(() => Released, x => x.CorrelateById(context => context.Message.ConfigurationId));

            Initially(
                When(ConfigurationSubmitted)
                    .Publish(context => context.Init<ImportConfiguration>(new { OrderId = context.Saga.CorrelationId }))
                    .TransitionTo(Importing));

            During(Importing,
                When(ConfigurationImported)
                    .Publish(context => new StartActions()
                    {
                        ConfigurationId = context.Saga.CorrelationId
                    })
                    .TransitionTo(DoingActions));

            During(DoingActions,
                When(ActionsDone)
                    .Publish(context => new ReleaseConfiguration()
                    {
                        ConfigurationId = context.Saga.CorrelationId
                    })
                    .TransitionTo(Releasing));

            During(Releasing,
                When(Released)
                    .TransitionTo(Completed));
        }

        public State Importing { get; private set; }
        public State DoingActions { get; }
        public State Releasing { get; }
        public State Completed { get; }

        public Event<ConfigurationSubmitted> ConfigurationSubmitted { get; }
        public Event<ConfigurationImported> ConfigurationImported { get; }
        public Event<ActionsDone> ActionsDone { get; }
        public Event<Released> Released { get; }
    }
}

using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.StateMachine
{
  public class ConfigurationStateMachine : MassTransitStateMachine<ConfigurationState>
  {
    public ConfigurationStateMachine()
    {
      InstanceState(x => x.CurrentState);

      Event(() => ConfigurationSubmitted, x => x.CorrelateById(context => context.Message.ConfigurationId));
      Event(() => ConfigurationImported, x => x.CorrelateById(context => context.Message.ConfigurationId));
      Event(() => ActionsDone, x => x.CorrelateById(context => context.Message.ConfigurationId));
      Event(() => Released, x => x.CorrelateById(context => context.Message.ConfigurationId));
      Event(() => ConfigurationFaulted, x => x.CorrelateById(context => context.Message.ConfigurationId));

      Initially(
          When(ConfigurationSubmitted)
              .Then(context => // This bit makes sure that the demo case we have in the ConfigurationSubmitted event gets stored in our state instance
              {
                var instance = context.Saga;
                var message = context.Message;

                instance.DemoCase = message.DemoCase;
              })
              .Publish(context => new ImportConfiguration() // Todo try async (right now we cant process multiple configurations at the same time
              {
                ConfigurationId = context.Saga.CorrelationId,
                DemoCase = context.Saga.DemoCase
              })
              .TransitionTo(Importing));

      During(Importing,
          When(ConfigurationImported)
              .Publish(context => new StartActions()
              {
                ConfigurationId = context.Saga.CorrelationId,
                DemoCase = context.Saga.DemoCase
              })
              .TransitionTo(DoingActions));

      During(DoingActions,
          When(ActionsDone)
              .Publish(context => new ReleaseConfiguration()
              {
                ConfigurationId = context.Saga.CorrelationId,
                DemoCase = context.Saga.DemoCase
              })
              .TransitionTo(Releasing));

      During(Releasing,
          When(Released)
              .TransitionTo(Completed));

      DuringAny(
        When(ConfigurationFaulted)
          .TransitionTo(Error));
    }

    public State Importing { get; }
    public State DoingActions { get; }
    public State Releasing { get; }
    public State Completed { get; }
    public State Error { get; }

    public Event<ConfigurationSubmitted> ConfigurationSubmitted { get; }
    public Event<ConfigurationImported> ConfigurationImported { get; }
    public Event<ActionsDone> ActionsDone { get; }
    public Event<Released> Released { get; }
    public Event<ConfigurationFaulted> ConfigurationFaulted { get; }
  }
}

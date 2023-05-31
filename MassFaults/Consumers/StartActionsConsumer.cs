using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.Consumers
{
  public class StartActionsConsumer : IConsumer<StartActions>
  {
    public Task Consume(ConsumeContext<StartActions> context)
    {
      Console.WriteLine($"{nameof(StartActionsConsumer)} received configuration '{context.Message.ConfigurationId}' (DemoCase: {context.Message.DemoCase})");

      switch (context.Message.DemoCase)
      {
        case Models.DemoCase.InvalidAction:
          string invalidMessage = "Action is invalid";
          Console.WriteLine(invalidMessage);
          return Task.FromException(new ApplicationException(invalidMessage));
        case Models.DemoCase.TimedOutAction:
          string timedOutMessage = "Action timed out";
          Console.WriteLine(timedOutMessage);
          return Task.FromException(new TimeoutException(timedOutMessage));
        default:
          Console.WriteLine($"Publishing {nameof(ActionsDone)}");
          context.Publish(new ActionsDone()
          {
            ConfigurationId = context.Message.ConfigurationId
          });
          break;
      }

      return Task.CompletedTask;
    }
  }
}

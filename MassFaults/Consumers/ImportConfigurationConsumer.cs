using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;

namespace MassFaults.Consumers
{
  public class ImportConfigurationConsumer : IConsumer<ImportConfiguration>
  {
    public Task Consume(ConsumeContext<ImportConfiguration> context)
    {
      Console.WriteLine($"{nameof(ImportConfigurationConsumer)} received configuration '{context.Message.ConfigurationId}' (DemoCase: {context.Message.DemoCase})");

      switch (context.Message.DemoCase)
      {
        case Models.DemoCase.InvalidImport:
          string invalidMessage = "Import is invalid";
          Console.WriteLine(invalidMessage);
          return Task.FromException(new ApplicationException(invalidMessage));
        case Models.DemoCase.TimedOutImport:
          string timedOutMessage = "Import timed out";
          Console.WriteLine(timedOutMessage);
          return Task.FromException(new TimeoutException(timedOutMessage));
        default:
          Console.WriteLine($"Publishing {nameof(ConfigurationImported)}");
          context.Publish(new ConfigurationImported()
          {
            ConfigurationId = context.Message.ConfigurationId
          });
          break;
      }

      return Task.CompletedTask;
    }
  }
}

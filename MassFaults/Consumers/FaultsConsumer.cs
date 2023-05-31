using MassFaults.Models.Events;
using MassFaults.Models.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassFaults.Consumers
{
  public class FaultsConsumer : IConsumer<Fault<ImportConfiguration>>, IConsumer<Fault<StartActions>>, IConsumer<Fault<ReleaseConfiguration>>
  {
    public Task Consume(ConsumeContext<Fault<ImportConfiguration>> context)
    {
      Console.WriteLine($"{nameof(ImportConfiguration)} with id '{context.Message.Message.ConfigurationId}' faulted.");// todo log exception

      FaultConfiguration(context, context.Message.Message.ConfigurationId);

      return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<Fault<StartActions>> context)
    {
      Console.WriteLine($"{nameof(StartActions)} with id '{context.Message.Message.ConfigurationId}' faulted.");

      FaultConfiguration(context, context.Message.Message.ConfigurationId);

      return Task.CompletedTask;
    }

    public Task Consume(ConsumeContext<Fault<ReleaseConfiguration>> context)
    {
      Console.WriteLine($"{nameof(ReleaseConfiguration)} with id '{context.Message.Message.ConfigurationId}' faulted.");

      FaultConfiguration(context, context.Message.Message.ConfigurationId);

      return Task.CompletedTask;
    }

    private void FaultConfiguration(IPublishEndpoint endpoint, Guid configurationId)
    {
      endpoint.Publish(new ConfigurationFaulted()
      {
        ConfigurationId = configurationId
      });
    }
  }
}

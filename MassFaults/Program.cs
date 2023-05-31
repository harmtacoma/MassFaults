using MassFaults.Configuration;
using MassFaults.Consumers;
using MassFaults.StateMachine;
using MassTransit;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
      services
          .AddMassTransit(config =>
          {
            config.AddConsumer<ImportConfigurationConsumer, DiscardFaultedConsumerDefinition<ImportConfigurationConsumer>>();
            config.AddConsumer<StartActionsConsumer, DiscardFaultedConsumerDefinition<StartActionsConsumer>>();
            config.AddConsumer<ReleaseConfigurationConsumer, DiscardFaultedConsumerDefinition<ReleaseConfigurationConsumer>>();
            config.AddConsumer<FaultsConsumer>();
            config.SetInMemorySagaRepositoryProvider();
            config.AddSagaStateMachine<ConfigurationStateMachine, ConfigurationState>();
            config.UseDefaultRabbitMq(hostContext.Configuration);
          });
    })
    .Build();

await host.RunAsync();
using MassFaults.Configuration;
using MassFaults.Console;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddMassTransit(config =>
            {
                config.UseDefaultRabbitMq(hostContext.Configuration);
            })
            .AddHostedService<App>();
    })
    .Build();

await host.RunAsync();
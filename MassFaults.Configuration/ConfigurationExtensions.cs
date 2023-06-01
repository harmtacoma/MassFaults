using MassTransit;
using Microsoft.Extensions.Configuration;

namespace MassFaults.Configuration
{
    public static class ConfigurationExtensions
    {
        public static RabbitMqOptions? GetRabbitMqOptions(this IConfiguration configuration)
        {
            return configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();
        }

        public static void UseDefaultRabbitMq(this IBusRegistrationConfigurator config, IConfiguration configuration)
        {
            var rabbitMqOptions = configuration.GetRabbitMqOptions() ?? throw new ApplicationException("No RabbitMQ options configured");

            config.UsingRabbitMq((context, rabbitMqConfig) =>
            {
                rabbitMqConfig.Host(rabbitMqOptions.Host, "/", hostConfig =>
                {
                    hostConfig.Username(rabbitMqOptions.Username);
                    hostConfig.Password(rabbitMqOptions.Password);
                });
                rabbitMqConfig.ConcurrentMessageLimit = 3;
                rabbitMqConfig.UseDefaultIncrementalRetryPolicy();
                rabbitMqConfig.ConfigureEndpoints(context);
            });
        }

        public static void UseDefaultIncrementalRetryPolicy(this IRabbitMqBusFactoryConfigurator rabbitMqConfig)
        {
            rabbitMqConfig.UseMessageRetry(r =>
            {
                r.Intervals(100, 1000, 5000, 15000);
                r.Ignore<ApplicationException>();
                r.Ignore<ArgumentException>();
                r.Ignore<ArgumentNullException>();
                r.Ignore<ArgumentOutOfRangeException>();
            });
        }
    }
}

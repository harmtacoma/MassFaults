using MassFaults.Models.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace MassFaults.Console
{
    public class App : IHostedService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public App(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                System.Console.WriteLine("Press '1' to publish an event. Press any other key to exit.");
                while (!cancellationToken.IsCancellationRequested)
                {
                    var key = System.Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.D1)
                    {
                        SubmitNewConfiguration().GetAwaiter().GetResult();
                    }
                    else
                    {
                        break;
                    }
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task SubmitNewConfiguration()
        {
            var newConfiguration = new ConfigurationSubmitted()
            {
                ConfigurationId = Guid.NewGuid()
            };

            await _publishEndpoint.Publish(newConfiguration);

            System.Console.WriteLine("Event published.");
        }
    }
}

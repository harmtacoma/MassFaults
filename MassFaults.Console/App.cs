using MassFaults.Models;
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
            await Task.Run(async () =>
            {
                System.Console.WriteLine("Press any number below to send an event for a specific demo case:");
                System.Console.WriteLine("1 - Success");
                System.Console.WriteLine("2 - InvalidImport");
                System.Console.WriteLine("3 - TimedOutImport");
                System.Console.WriteLine("4 - InvalidAction");
                System.Console.WriteLine("5 - TimedOutAction");
                System.Console.WriteLine("6 - InvalidRelease");
                System.Console.WriteLine("7 - TimedOutRelease");
                System.Console.WriteLine("8 - InvalidActionTwo");
                System.Console.WriteLine("9 - InvalidActionThree");
                System.Console.WriteLine("Press 'c' to exit");

                while (!cancellationToken.IsCancellationRequested)
                {
                    var key = System.Console.ReadKey(intercept: true).Key;

                    switch (key)
                    {
                        case ConsoleKey.D1:
                            await SubmitNewConfigurationAsync();
                            break;
                        case ConsoleKey.D2:
                            await SubmitNewConfigurationAsync(DemoCase.InvalidImport);
                            break;
                        case ConsoleKey.D3:
                            await SubmitNewConfigurationAsync(DemoCase.TimedOutImport);
                            break;
                        case ConsoleKey.D4:
                            await SubmitNewConfigurationAsync(DemoCase.InvalidAction);
                            break;
                        case ConsoleKey.D5:
                            await SubmitNewConfigurationAsync(DemoCase.TimedOutAction);
                            break;
                        case ConsoleKey.D6:
                            await SubmitNewConfigurationAsync(DemoCase.InvalidRelease);
                            break;
                        case ConsoleKey.D7:
                            await SubmitNewConfigurationAsync(DemoCase.TimedOutRelease);
                            break;
                        case ConsoleKey.D8:
                            await SubmitNewConfigurationAsync(DemoCase.InvalidActionTwo);
                            break;
                        case ConsoleKey.D9:
                            await SubmitNewConfigurationAsync(DemoCase.InvalidActionThree);
                            break;
                    }

                    if (key == ConsoleKey.C)
                    {
                        System.Console.WriteLine("Exiting");
                    }
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task SubmitNewConfigurationAsync(DemoCase demoCase = DemoCase.Success)
        {
            var newConfiguration = new ConfigurationSubmitted
            {
                ConfigurationId = Guid.NewGuid(),
                DemoCase = demoCase
            };

            await _publishEndpoint.Publish(newConfiguration);

            System.Console.WriteLine($"Event published, demo case: {demoCase}");
        }
    }
}

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
      await Task.Run(() =>
      {
        System.Console.WriteLine("Press any number below to send an event for a specific demo case:");
        System.Console.WriteLine("1 - Success");
        System.Console.WriteLine("2 - InvalidImport");
        System.Console.WriteLine("3 - TimedOutImport");
        System.Console.WriteLine("4 - InvalidAction");
        System.Console.WriteLine("5 - TimedOutAction");
        System.Console.WriteLine("6 - InvalidRelease");
        System.Console.WriteLine("7 - TimedOutRelease");
        System.Console.WriteLine("Press 'c' to exit");

        while (!cancellationToken.IsCancellationRequested)
        {
          var key = System.Console.ReadKey(intercept: true).Key;
          
          switch (key)
          {
            case ConsoleKey.D1:
              SubmitNewConfiguration().GetAwaiter().GetResult();
              break;
            case ConsoleKey.D2:
              SubmitNewConfiguration(DemoCase.InvalidImport).GetAwaiter().GetResult();
              break;
            case ConsoleKey.D3:
              SubmitNewConfiguration(DemoCase.TimedOutImport).GetAwaiter().GetResult();
              break;
            case ConsoleKey.D4:
              SubmitNewConfiguration(DemoCase.InvalidAction).GetAwaiter().GetResult();
              break;
            case ConsoleKey.D5:
              SubmitNewConfiguration(DemoCase.TimedOutAction).GetAwaiter().GetResult();
              break;
            case ConsoleKey.D6:
              SubmitNewConfiguration(DemoCase.InvalidRelease).GetAwaiter().GetResult();
              break;
            case ConsoleKey.D7:
              SubmitNewConfiguration(DemoCase.TimedOutRelease).GetAwaiter().GetResult();
              break;
            default:
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

    private async Task SubmitNewConfiguration(DemoCase demoCase = DemoCase.Success)
    {
      var newConfiguration = new ConfigurationSubmitted()
      {
        ConfigurationId = Guid.NewGuid(),
        DemoCase = demoCase
      };

      await _publishEndpoint.Publish(newConfiguration);

      System.Console.WriteLine("Event published");
    }
  }
}

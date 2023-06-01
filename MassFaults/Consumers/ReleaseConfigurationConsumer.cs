using MassFaults.Models.Commands;
using MassFaults.Models.Events;
using MassTransit;

namespace MassFaults.Consumers
{
    public class ReleaseConfigurationConsumer : IConsumer<ReleaseConfiguration>
    {
        public Task Consume(ConsumeContext<ReleaseConfiguration> context)
        {
            Console.WriteLine($"{nameof(ReleaseConfigurationConsumer)} received configuration '{context.Message.ConfigurationId}' (DemoCase: {context.Message.DemoCase})");

            switch (context.Message.DemoCase)
            {
                case Models.DemoCase.InvalidRelease:
                    string invalidMessage = "Release is invalid";
                    Console.WriteLine(invalidMessage);
                    return Task.FromException(new ApplicationException(invalidMessage));
                case Models.DemoCase.TimedOutRelease:
                    string timedOutMessage = "Release timed out";
                    Console.WriteLine(timedOutMessage);
                    return Task.FromException(new TimeoutException(timedOutMessage));
                default:
                    Console.WriteLine($"Publishing {nameof(Released)}");
                    context.Publish(new Released()
                    {
                        ConfigurationId = context.Message.ConfigurationId
                    });
                    break;
            }

            return Task.CompletedTask;
        }
    }
}

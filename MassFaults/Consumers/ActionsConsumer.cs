using MassFaults.Models.Commands;
using MassFaults.Models.Events;
using MassTransit;

namespace MassFaults.Consumers
{
    public class ActionsConsumer : IConsumer<StartActions>, IConsumer<DoActionOne>, IConsumer<DoActionTwo>, IConsumer<DoActionThree>
    {
        public Task Consume(ConsumeContext<StartActions> context)
        {
            Console.WriteLine($"{nameof(ActionsConsumer)} received configuration '{context.Message.ConfigurationId}' (DemoCase: {context.Message.DemoCase})");

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
                    Console.WriteLine($"Publishing {nameof(DoActionOne)}");
                    context.Publish(new DoActionOne()
                    {
                        ConfigurationId = context.Message.ConfigurationId,
                        DemoCase = context.Message.DemoCase
                    });
                    break;
            }

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<DoActionOne> context)
        {
            Console.WriteLine($"Publishing {nameof(DoActionTwo)}");

            context.Publish(new DoActionTwo()
            {
                ConfigurationId = context.Message.ConfigurationId,
                DemoCase = context.Message.DemoCase
            });

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<DoActionTwo> context)
        {
            switch (context.Message.DemoCase)
            {
                case Models.DemoCase.InvalidActionTwo:
                    string invalidMessage = "Action 2 is invalid";
                    Console.WriteLine(invalidMessage);
                    return Task.FromException(new ApplicationException(invalidMessage));
                default:
                    Console.WriteLine($"Publishing {nameof(DoActionThree)}");
                    context.Publish(new DoActionThree()
                    {
                        ConfigurationId = context.Message.ConfigurationId,
                        DemoCase = context.Message.DemoCase
                    });
                    break;
            }

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<DoActionThree> context)
        {
            switch (context.Message.DemoCase)
            {
                case Models.DemoCase.InvalidActionThree:
                    string invalidMessage = "Action 3 is invalid";
                    Console.WriteLine(invalidMessage);
                    return Task.FromException(new ApplicationException(invalidMessage));
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

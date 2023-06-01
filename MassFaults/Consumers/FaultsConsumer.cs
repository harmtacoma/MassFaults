using MassFaults.Models.Commands;
using MassFaults.Models.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassFaults.Consumers
{
    public class FaultsConsumer : IConsumer<Fault<ImportConfiguration>>, IConsumer<Fault<StartActions>>, IConsumer<Fault<ReleaseConfiguration>>, IConsumer<Fault<IActionCommand>>
    {
        public Task Consume(ConsumeContext<Fault<ImportConfiguration>> context)
        {
            return FaultConfiguration(context, context.Message.Message.ConfigurationId, nameof(ImportConfiguration), context.Message.Exceptions.FirstOrDefault());
        }

        public Task Consume(ConsumeContext<Fault<StartActions>> context)
        {
            return FaultConfiguration(context, context.Message.Message.ConfigurationId, nameof(StartActions), context.Message.Exceptions.FirstOrDefault());
        }

        public Task Consume(ConsumeContext<Fault<ReleaseConfiguration>> context)
        {
            return FaultConfiguration(context, context.Message.Message.ConfigurationId, nameof(ReleaseConfiguration), context.Message.Exceptions.FirstOrDefault());
        }

        public Task Consume(ConsumeContext<Fault<IActionCommand>> context)
        {
            return FaultConfiguration(context, context.Message.Message.ConfigurationId, context.Message.FaultMessageTypes[0].Split(':').Last(), context.Message.Exceptions.FirstOrDefault());
        }

        private static Task FaultConfiguration(IPublishEndpoint endpoint, Guid configurationId, string messageType, ExceptionInfo? exceptionInfo)
        {
            Console.WriteLine($"Configuration '{configurationId}' faulted during {messageType}. Error: '{exceptionInfo?.Message}'");

            endpoint.Publish(new ConfigurationFaulted()
            {
                ConfigurationId = configurationId
            });

            return Task.CompletedTask;
        }
    }
}

using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassFaults
{
    public class DiscardFaultedConsumerDefinition<T> : ConsumerDefinition<T> where T : class, MassTransit.IConsumer
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<T> consumerConfigurator)
        {
            endpointConfigurator.DiscardFaultedMessages();
        }
    }
}

using MassTransit;

namespace MassFaults.Models
{
    public class ConfigurationState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
    }
}

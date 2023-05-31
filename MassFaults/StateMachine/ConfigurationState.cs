using MassFaults.Models;
using MassTransit;

namespace MassFaults.StateMachine
{
  public class ConfigurationState : SagaStateMachineInstance
  {
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public DemoCase DemoCase { get; set; }
  }
}

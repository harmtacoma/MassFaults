using MassFaults.Models.Events;
using MassFaults.StateMachine;
using MassTransit.Testing;
using MassTransit;

namespace MassFaults.Test
{
    public class ConfigurationStateMachineTests
    {
        [Fact]
        public async Task ShouldCreateInstances()
        {
            var stateMachine = new ConfigurationStateMachine();

            var harness = new InMemoryTestHarness();
            var saga = harness.StateMachineSaga<ConfigurationState, ConfigurationStateMachine>(stateMachine);

            await harness.Start();
            try
            {
                var configurationId = NewId.NextGuid();

                await harness.Bus.Publish(new ConfigurationSubmitted
                {
                    ConfigurationId = configurationId,
                    DemoCase = Models.DemoCase.Success
                });

                Assert.True(saga.Created.Select(x => x.CorrelationId == configurationId).Any());

                var instanceId = await saga.Exists(configurationId, x => x.Importing);
                Assert.NotNull(instanceId);

                var instance = saga.Sagas.Contains(instanceId.Value);
                Assert.Equal(Models.DemoCase.Success, instance.DemoCase);
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}

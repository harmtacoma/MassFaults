using MassFaults.Consumers;
using MassFaults.Models.Commands;
using MassFaults.Models.Events;
using MassTransit;
using MassTransit.Testing;

namespace MassFaults.Test
{
    public class ImportConfigurationConsumerTests
    {
        [Fact]
        public async Task ShouldImportConfigurations()
        {
            var harness = new InMemoryTestHarness();

            var consumer = harness.Consumer<ImportConfigurationConsumer>();

            try
            {
                await harness.Start();

                var configurationId = NewId.NextGuid();

                var importMessage = new ImportConfiguration
                {
                    ConfigurationId = configurationId,
                    DemoCase = Models.DemoCase.Success
                };

                await harness.Bus.Publish(importMessage);

                Assert.True(await harness.Consumed.Any<ImportConfiguration>());
                Assert.True(await consumer.Consumed.Any<ImportConfiguration>());
                Assert.True(await harness.Published.Any<ConfigurationImported>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
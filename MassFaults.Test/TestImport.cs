using MassFaults.Consumers;
using MassFaults.Models.Commands;
using MassFaults.Models.Events;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MassFaults.Test
{
    public class TestImport
    {
        [Fact]
        public async Task TestImportSuccess()
        {
            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(x =>
                {
                    x.AddConsumer<ImportConfigurationConsumer>();
                    x.AddConsumer<ActionsConsumer>();
                    x.AddConsumer<ReleaseConfigurationConsumer>();
                })
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();
            await harness.Start();
            var client = harness.GetRequestClient<ImportConfiguration>();
            var response = await client.GetResponse<ConfigurationImported>(new ImportConfiguration
            {
                ConfigurationId = Guid.NewGuid(),
                DemoCase = Models.DemoCase.Success
            });
            Assert.True(await harness.Sent.Any<ConfigurationImported>());
            Assert.True(await harness.Consumed.Any<ImportConfiguration>());
            var consumerHarness = harness.GetConsumerHarness<ImportConfigurationConsumer>();
            Assert.True(await consumerHarness.Consumed.Any<ImportConfiguration>());
        }
    }
}
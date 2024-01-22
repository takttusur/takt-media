using Microsoft.Extensions.Configuration;
using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Infrastructure.Events;

namespace TaktTusur.Media.Infrastructure.Tests
{
    public class VkEventsImporterTests
    {
        private VkApiOptions _options = new();

        public VkEventsImporterTests()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<VkEventsImporterTests>();

            var config = builder.Build();
            _options.Key = config["VkApiKey"];
        }


        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ImportAsync()
        {
            var vkApiClient = new VkApiClient(_options);
            var eventsImporter = new VkEventsImporter(vkApiClient);
            var events = await eventsImporter.ImportAsync("takt_tusur", 10, CancellationToken.None);
            Assert.That(events.Any());
        }
    }
}
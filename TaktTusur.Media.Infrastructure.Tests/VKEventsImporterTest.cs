using Microsoft.Extensions.Configuration;
using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Infrastructure.Events;

namespace TaktTusur.Media.Infrastructure.Tests
{
    public class VkEventsImporterTests
    {
        private VkApiOptions _options = new();
        private VkApiClient _vkApiClient;
        private VkEventsImporter _vkEventsImporter;

        public VkEventsImporterTests()
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<VkEventsImporterTests>();
            var config = builder.Build();

            _options.Key = config["VkApiKey"];
        }


        [SetUp]
        public void Setup()
        {
            _vkApiClient = new VkApiClient(_options);
            _vkEventsImporter = new VkEventsImporter(_vkApiClient);
        }

        [Test]
        public async Task ImportAsync()
        {
            var events = await _vkEventsImporter.ImportAsync("takt_tusur", CancellationToken.None, 10);

            Assert.AreEqual(1, events.Count(), "Количество публичных событий, среди последних 10 постов - 1");
        }
    }
}
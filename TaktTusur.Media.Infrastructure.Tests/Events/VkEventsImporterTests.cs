using Moq;
using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Clients.VkApi.Models;
using TaktTusur.Media.Infrastructure.Events;

namespace TaktTusur.Media.Infrastructure.Tests.Events;

[TestFixture]
public class VkEventsImporterTests
{
	[Test]
	public async Task ImportAsync_CorrectImportTest()
	{
		var api = new Mock<IVkApiClient>();
		api.Setup(x => x.GetGroupInfoAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(new VkGroupInfo { Id = 1, Name = "Group 1", StartDateTime = DateTimeOffset.Now, FinishDateTime = DateTimeOffset.Now});

		var importer = new VkEventsImporter(api.Object);
		var result = await importer.ImportAsync("test", CancellationToken.None);
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Title, Is.EqualTo("Group 1"));
	}

	[Test]
	public async Task ImportAsync_NotEventTest()
	{
		var api = new Mock<IVkApiClient>();
		api.Setup(x => x.GetGroupInfoAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(new VkGroupInfo() { Id = 2, Name = "Test", StartDateTime = null });

		var importer = new VkEventsImporter(api.Object);
		var result = await importer.ImportAsync("test", CancellationToken.None);
		Assert.That(result, Is.Null);
	}
}
using Moq;
using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Clients.VkApi.Models;
using TaktTusur.Media.Clients.VkApi.Models.Common;
using TaktTusur.Media.Infrastructure.Analyzers;

namespace TaktTusur.Media.Infrastructure.Tests.Analyzers;

[TestFixture]
public class VkAnalyzerTests
{
	[Test]
	public async Task FindPublicEvent_PostWithAttachedEvent()
	{
		var client = new Mock<IVkApiClient>();
		var service = new VkAnalyzer(client.Object);
		var post = new WallPost()
		{
			PostAttachment = new List<Attachment>
			{
				new Attachment
				{
					Type = "event",
					Event = new VkEvent
					{
						Id = 123,
						EventStartDateTime = 1234567890
					}
				}
			}
		};

		var result = await service.FindPublicEvent(post, CancellationToken.None);
		
		Assert.That(result, Is.Not.Null, "Attached event is not recognized");
		Assert.That(result, Is.EqualTo("public123"), "Public event page is not correct");
	}

	[Test]
	public async Task FindPublicEvent_PostWithRepost()
	{
		var testGroupInfo = new VkGroupInfo()
		{
			StartDateTime = DateTimeOffset.Now,
			Id = 555666
		};
		var client = new Mock<IVkApiClient>();
		client.Setup(x => x.GetGroupInfoAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(testGroupInfo);
		var service = new VkAnalyzer(client.Object);
		var post = new WallPost()
		{
			InnerPosts =
			{
				new WallPost()
				{
					SourceId = 555666
				}
			}
		};

		var result = await service.FindPublicEvent(post, CancellationToken.None);
		
		Assert.That(result, Is.Not.Null, "Attached event is not recognized");
		Assert.That(result, Is.EqualTo("public555666"), "Public event page is not correct");
	}

	[Test]
	public async Task FindPublicEvent_PostWithoutEvent()
	{
		var client = new Mock<IVkApiClient>();
		var service = new VkAnalyzer(client.Object);
		var post = new WallPost();

		var result = await service.FindPublicEvent(post, CancellationToken.None);
		
		Assert.That(result, Is.Null, "The post doesn't contain events, result should be null");
	}
}
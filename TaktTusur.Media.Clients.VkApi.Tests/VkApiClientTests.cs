using NUnit.Framework;
using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Clients.VkApi.Tests
{
    [TestFixture]
    public class VkApiClientTests
    {
        private readonly VkApiClient _client;
        
        public VkApiClientTests()
        {
            var config = TestsConfiguration.FromUserSecrets();

            _client = new VkApiClient(new VkApiOptions()
            {
                Key = config.VkApiKey
            });
        }

        [Test]
        [TestCase("takttusur.testevent", "TaktTusur test event")]
        public async Task CheckingTitleVkGroup(string groupId, string title)
        {
            var info = await _client.GetGroupInfoAsync(groupId, CancellationToken.None);

            Assert.That(info.GroupName, Is.EqualTo(title), "Wrong group name");
            Assert.That(info.StartDateTime, Is.Not.Null, "Wrong start date");
            Assert.That(info.FinishDateTime, Is.Not.Null, "Wrong finish date");
        }

        [Test]
        [TestCase("takttusur.testevent", 3)]
        public async Task CheckingQuantityOfPostsFromVkGroupWall(string groupId, int numberOfPosts)
        {
            
            var testResult = await _client.GetPostsAsync(groupId, 1, CancellationToken.None);

            Assert.That(testResult.Count, Is.EqualTo(numberOfPosts), "Wrong total items count");
        }

        [Test]
        [TestCase("takttusur.testevent")]
        public async Task CheckingPhotoIdFromAttachmentsFromVkGroupWallPost(string groupId)
        {
            var testResult = await _client.GetPostsAsync(groupId, 1, CancellationToken.None);

            Assert.That(testResult.Posts[0].PostAttachment[0].Type, Is.EqualTo("photo"), "Wrong type of first attachment");
        }

        [Test]
        [TestCase("takttusur.testevent000000000000")]
        public void CheckingThrowingExceptionGetGroupInfo(string groupId)
        {
            Assert.ThrowsAsync<VkApiException>(async () =>
            {
                var unused = await _client.GetGroupInfoAsync(groupId, CancellationToken.None);
            });
        }

        [Test]
        [TestCase("takttusur.testevent00000000000")]
        public void CheckingThrowingExceptionGetPost(string groupId)
        {
            Assert.ThrowsAsync<VkApiException>(async () =>
            {
                var unused = await _client.GetPostsAsync(groupId,1, CancellationToken.None);
            });
        }
    }
}
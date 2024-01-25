using NUnit.Framework;
using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Clients.VkApi.Tests
{
    [TestFixture]
    public class VkApiClientTests
    {
        private const string _testVkEvent = "takttusur.testevent";
        private const string _testVkGroup = "takttusur.testpublic";
        private const string _testVkIdNotExists = "takttusur.testevent00000000";
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
        [TestCase(_testVkEvent, "TaktTusur test event")]
        public async Task CheckingTitleVkGroup(string groupId, string title)
        {
            var info = await _client.GetGroupInfoAsync(groupId, CancellationToken.None);

            Assert.That(info.GroupName, Is.EqualTo(title), "Wrong group name");
            Assert.That(info.StartDateTime, Is.Not.Null, "Wrong start date");
            Assert.That(info.FinishDateTime, Is.Not.Null, "Wrong finish date");
        }

        [Test]
        [TestCase(_testVkEvent, 3)]
        public async Task CheckingQuantityOfPostsFromVkGroupWall(string groupId, int numberOfPosts)
        {
            
            var testResult = await _client.GetPostsAsync(groupId, 1, CancellationToken.None);

            Assert.That(testResult.Count, Is.EqualTo(numberOfPosts), "Wrong total items count");
        }

        [Test]
        [TestCase(_testVkEvent)]
        public async Task CheckingPhotoIdFromAttachmentsFromVkGroupWallPost(string groupId)
        {
            var testResult = await _client.GetPostsAsync(groupId, 1, CancellationToken.None);

            Assert.That(testResult.Posts[0].PostAttachment[0].Type, Is.EqualTo("photo"), "Wrong type of first attachment");
        }

        [Test]
        [TestCase(_testVkIdNotExists)]
        public void CheckingThrowingExceptionGetGroupInfo(string groupId)
        {
            Assert.ThrowsAsync<VkApiException>(async () =>
            {
                var unused = await _client.GetGroupInfoAsync(groupId, CancellationToken.None);
            });
        }

        [Test]
        [TestCase(_testVkIdNotExists)]
        public void CheckingThrowingExceptionGetPost(string groupId)
        {
            Assert.ThrowsAsync<VkApiException>(async () =>
            {
                var unused = await _client.GetPostsAsync(groupId,1, CancellationToken.None);
            });
        }

        [Test]
        [TestCase(_testVkGroup, 4, 224381871)]
        public async Task GetPostWithAttachedEventTest(string groupId, long eventPostId, long publicEventId)
        {
            var posts = await _client.GetPostsAsync(groupId,10, CancellationToken.None);
            
            Assert.That(posts.Count, Is.Positive, "Nothing posts on the wall of test VK group");

            var post = posts.Posts.FirstOrDefault(p => p.Id == eventPostId);
            
            Assert.That(posts, Is.Not.Null, "Post not found by Id:" + eventPostId);

            var attachment = post.PostAttachment.FirstOrDefault(a => a.Event != null);
            
            Assert.That(attachment.Event.Id, Is.EqualTo(publicEventId), "Invalid EventId");
        }

        [Test]
        [TestCase(_testVkGroup, 0, 224381871)]
        public async Task GetRepostFromEventTest(string groupId, long repostPostId, long repostSourceId)
        {
            var posts = await _client.GetPostsAsync(groupId,10, CancellationToken.None);
            
            Assert.That(posts.Count, Is.Positive, "Nothing posts on the wall of test VK group");

            var repost = posts.Posts.FirstOrDefault(p => p.InnerPosts.Any());
            
            Assert.That(repost, Is.Not.Null, "Repost is not found");
            Assert.That(repost.InnerPosts.First().SourceId, Is.EqualTo(repostSourceId), "Post reposted from unknown source");
        }
    }
}
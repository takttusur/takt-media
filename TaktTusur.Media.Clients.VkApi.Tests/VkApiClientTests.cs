using Microsoft.Extensions.Configuration;

namespace TaktTusur.Media.Clients.VkApi.Tests;

public class VkApiClientTests
{
    private VkApiOptions _options = new VkApiOptions();

    public VkApiClientTests()
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<VkApiClientTests>();
        var config = builder.Build();

        _options.Key = config["VkApiKey"];
    }
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CheckingTitleVkGroup()
    {
        var clientApi = new VkApiClient(_options); 
        var info = await clientApi.GetGroupInfoAsync("takt_tusur", CancellationToken.None);

        Assert.AreEqual("Туристско-Альпинистский клуб ТУСУРа (ТАКТ)", info.GroupName, "неправильное название группы");
    }

    [Test]
    public async Task CheckingQuantityOfPostsFromVkGroupWall()
    {
        var clientApi = new VkApiClient(_options);
        var testResult = await clientApi.GetPostsAsync("takt_tusur", CancellationToken.None, 5);

        Assert.AreEqual(2413, testResult.Count, "неправильное количестов постов");
    }

    [Test]
    public async Task CheckingPhotoIdFromAttachmentsFromVkGroupWallPost()
    {
        var clientApi = new VkApiClient(_options);
        var testResult = await clientApi.GetPostsAsync("takt_tusur", CancellationToken.None, 5);

        Assert.AreEqual(457265786, testResult.Posts[1].PostAttachment[0].Photo.Id, "неправильный ID");
    }
    
    [Test]
    public async Task CheckingThrowingExceptionGetGroupInfo()
    {
        var clientApi = new VkApiClient(_options);

        Assert.ThrowsAsync<VkApiException>(async () =>
        {
            var info = await clientApi.GetGroupInfoAsync("takttusur", CancellationToken.None);
        });
    }

    [Test]
    public async Task CheckingThrowingExceptionGetPost()
    {
        var clientApi = new VkApiClient(_options);
        Assert.ThrowsAsync<VkApiException>(async () =>
        {
            var testResult = await clientApi.GetPostsAsync("takttusur", CancellationToken.None, 5);
        });
    }
}
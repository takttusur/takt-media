using Microsoft.Extensions.Configuration;

namespace TaktTusur.Media.Clients.VkApi.Tests;

public class VkApiClientTests
{
    private VkApiOptions _options = new VkApiOptions();

    public VkApiClientTests()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<VkApiClientTests>();

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
        var clientapi = new VkApiClient(_options, "takt_tusur"); 
        var info = await clientapi.GetGroupInfoAsync(CancellationToken.None);
        Assert.AreEqual("Туристско-Альпинистский клуб ТУСУРа (ТАКТ)", info.GroupName, "неправильное название группы");
            
    }

    [Test]
    public async Task CheckingQuantityOfPostsFromVkGroupWall ()
    {
        var clientApi = new VkApiClient(_options, "takt_tusur");
        var testResult = await clientApi.GetPostsAsync(CancellationToken.None);
        Assert.AreEqual(2406, testResult.Count, "неправильное количестов постов");
    }

    [Test]
    public async Task CheckingPhotoIdFromAttachmentsFromVkGroupWallPost()
    {
        var clientApi = new VkApiClient(_options, "takt_tusur");
        var testResult = await clientApi.GetPostsAsync(CancellationToken.None);
        Assert.AreEqual(457265786, testResult.Posts[1].PostAttachment[0].Photo.Id, "неправильный ID");
    }
}
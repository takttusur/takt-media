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
    public async Task  Test1()
    {
        var clientapi = new VkApiClient(_options, "takt_tusur"); 
        var info = await clientapi.GetGroupInfoAsync(CancellationToken.None);
        Assert.AreEqual("Туристско-Альпинистский клуб ТУСУРа (ТАКТ)", info.GroupName, "неправильное название группы");
            
    }

    [Test]
    public async Task Test2()
    {
        var clientapi = new VkApiClient(_options, "takt_tusur");
        var testr = await clientapi.GetPostsAsync(CancellationToken.None);
        Assert.AreEqual(2404, testr.Count, "неправильное количестов постов");
    }

    [Test]
    public async Task Test3()
    {
        var clientapi = new VkApiClient(_options, "takt_tusur");
        var testr = await clientapi.GetPostsAsync(CancellationToken.None);
        Assert.AreEqual(457265785, testr.Posts[0].PostAttachment[0].Photo.Id, "неправильный ID");
    }
}
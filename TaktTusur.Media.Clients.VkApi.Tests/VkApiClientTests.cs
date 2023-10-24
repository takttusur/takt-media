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
        var clientapi = new VkApiClient(_options); 
        var info = await clientapi.GetGroupInfoAsync("takt_tusur", CancellationToken.None);
        Assert.AreEqual("Туристско-Альпинистский клуб ТУСУРа (ТАКТ)", info.GroupName, "неправильное название группы");
            
    }
}
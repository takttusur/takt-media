using Microsoft.Extensions.Configuration;

namespace TaktTusur.Media.Clients.VkApi.Tests;

public class TestsConfiguration
{
	public string VkApiKey { get; private set; }
	
	public static TestsConfiguration FromUserSecrets()
	{
		var builder = new ConfigurationBuilder().AddUserSecrets<TestsConfiguration>();
		var config = builder.Build();

		if (string.IsNullOrEmpty(config[nameof(VkApiKey)]))
			throw new ArgumentException($"Please, add {nameof(VkApiKey)} to user secrets");
		
		return new TestsConfiguration { VkApiKey = config[nameof(VkApiKey)] };
	}
}
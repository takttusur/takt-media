using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Infrastructure.Analyzers;

public class VkAnalyzer : IVkAnalyzer
{
	public Task<bool> IsFromPublicEventAsync(WallPost post, CancellationToken token)
	{
		throw new NotImplementedException();
	}
}
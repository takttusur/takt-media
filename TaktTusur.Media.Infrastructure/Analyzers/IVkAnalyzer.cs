using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Infrastructure.Analyzers;

public interface IVkAnalyzer
{
	public Task<string?> FindPublicEvent(WallPost posts, CancellationToken token);
}
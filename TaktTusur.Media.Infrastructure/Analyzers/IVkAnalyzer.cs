using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Infrastructure.Analyzers;

public interface IVkAnalyzer
{
	public Task<bool> IsFromPublicEventAsync(WallPost posts, CancellationToken token);
}
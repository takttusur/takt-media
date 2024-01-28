using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Infrastructure.Analyzers;

public class VkAnalyzer : IVkAnalyzer
{
	private readonly IVkApiClient _vkApiClient;

	public VkAnalyzer(IVkApiClient vkApiClient)
	{
		_vkApiClient = vkApiClient;
	}
	
	/// <summary>
	/// Проверяет, есть ли в посте умопинание мероприятия:
	/// Мероприятие прикреплено, или репост из мероприятия.
	/// </summary>
	/// <param name="post">Пост для анализа</param>
	/// <param name="token"></param>
	/// <returns>ID мероприятия, если найдено</returns>
	public async Task<string?> FindPublicEvent(WallPost post, CancellationToken token)
	{
		var attachedEvent = post.PostAttachment.FirstOrDefault(a => a.Type == "event")?.Event;
		if (attachedEvent != null)
		{
			return $"public{attachedEvent.Id}";
		}

		if (!post.InnerPosts.Any())
		{
			return null;
		}

		var groupsInfo = post.InnerPosts
			.Select(p => _vkApiClient.GetGroupInfoAsync($"public{p.SourceId}", token))
			.ToArray();
		var result = await Task.WhenAll<VkGroupInfo>(groupsInfo);

		var e = result.FirstOrDefault(r => r.StartDateTime != null);
		
		return e != null ? $"public{e.Id}" : null;
	}
}
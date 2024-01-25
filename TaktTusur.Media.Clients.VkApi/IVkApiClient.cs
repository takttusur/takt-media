using TaktTusur.Media.Clients.VkApi.Models;

namespace TaktTusur.Media.Clients.VkApi;

public interface IVkApiClient
{
    Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken);

    Task<VkPosts> GetPostsAsync(string groupId, int maxPosts, CancellationToken cancellationToken);
}
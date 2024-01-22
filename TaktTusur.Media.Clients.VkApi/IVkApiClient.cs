namespace TaktTusur.Media.Clients.VkApi;

public interface IVkApiClient
{
    Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken, List<string> fields);

    Task<VkPost> GetPostsAsync(string groupId, CancellationToken cancellationToken, int count);
}
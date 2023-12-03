namespace TaktTusur.Media.Clients.VkApi;

public interface IVkApiClient
{
    Task<VkGroupInfo> GetGroupInfoAsync(CancellationToken cancellationToken);
    Task<VkPost> GetPostsAsync(CancellationToken cancellationToken);
}

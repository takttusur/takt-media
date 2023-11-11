namespace TaktTusur.Media.Clients.VkApi;

public interface IVkApiClient
{
    Task<VkGroupInfo> GetGroupInfoAsync(CancellationToken cancellationToken);

    //TODO: как я понял он должен возвращать список, не успел попробовать
    Task<VkPost> GetPostsAsync(string groupId, CancellationToken cancellationToken);
}

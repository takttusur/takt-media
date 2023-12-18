using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public class VkEventsImporter: IVkEventsImporter, IVkApiClient
{
    public VkEventsImporter(IVkApiClient apiClient)
    {
        
    }

    public Task<PublicEvent> ImportAsync(string vkGroupId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<VkPost> GetPostsAsync(string groupId, int count, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
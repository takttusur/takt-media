using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public class VkEventsImporter : IVkEventsImporter, IVkApiClient
{
    private VkApiClient _vkApiClient;

    private List<string> _fields = new() { "start_date", "finish_date" };

    public VkEventsImporter(VkApiClient apiClient)
    {
        _vkApiClient = apiClient;
    }


    public async Task<List<PublicEvent>> ImportAsync(string groupId, int postsCount, CancellationToken cancellationToken)
    {
        return await AnalyzeAsync(await GetPostsAsync(groupId, postsCount, cancellationToken), cancellationToken);
    }

    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken, List<string> fields)
    {
        return await _vkApiClient.GetGroupInfoAsync(groupId, cancellationToken, fields);
    }

    public async Task<VkPost> GetPostsAsync(string groupId, int count, CancellationToken cancellationToken)
    {
        return await _vkApiClient.GetPostsAsync(groupId, count, cancellationToken);
    }


    private async Task<List<PublicEvent>> AnalyzeAsync(VkPost vkPost, CancellationToken cancellationToken)
    {
        VkGroupInfo vkGroupInfo;
        List<PublicEvent> publicEvents = new();

        foreach (var post in vkPost.Posts.Where(p => p.PostType == "reply"))
            if ((vkGroupInfo = await _vkApiClient.GetGroupInfoAsync(post.PostCopyrightNotes.Id, cancellationToken, _fields)).GroupType == "event")
            {
                PublicEvent publicEvent = new() { 
                    EventStartDateTime = vkGroupInfo.StartDateTime,
                    EventEndDateTime = vkGroupInfo.FinishDateTime,
                    EventTitle = vkGroupInfo.Description,
                    EventURL = post.PostURL
                };
                publicEvents.Add(publicEvent);
            }

        return publicEvents;
    }
}
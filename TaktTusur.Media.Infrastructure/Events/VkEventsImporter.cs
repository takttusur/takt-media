using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public class VkEventsImporter : IVkEventsImporter, IVkApiClient
{
    private VkApiClient _vkApiClient;


    public VkEventsImporter(VkApiClient apiClient)
    {
        _vkApiClient = apiClient;
    }


    public async Task<List<PublicEvent>> ImportAsync(string groupId, CancellationToken cancellationToken, int postsCount = 5)
    {
        return await AnalyzeAsync(await GetPostsAsync(groupId, cancellationToken, postsCount), cancellationToken);
    }

    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken, List<string> fields)
    {
        return await _vkApiClient.GetGroupInfoAsync(groupId, cancellationToken, fields);
    }

    public async Task<VkPost> GetPostsAsync(string groupId, CancellationToken cancellationToken, int count = 5)
    {
        return await _vkApiClient.GetPostsAsync(groupId, cancellationToken, count);
    }


    private async Task<List<PublicEvent>> AnalyzeAsync(VkPost vkPost, CancellationToken cancellationToken)
    {
        VkGroupInfo vkGroupInfo;
        List<PublicEvent> publicEvents = new();
        List<string> _fields = new() { "start_date", "finish_date" };

        foreach (var post in vkPost.Posts.Where(p => p.PostAttachment.Any(t => t.Type == "event")))
        {
            vkGroupInfo = await GetGroupInfoAsync(post.PostAttachment.FirstOrDefault(p => p.Type == "event").Event.Id.ToString(), cancellationToken, _fields);

            PublicEvent publicEvent = new()
            {
                EventStartDateTime = vkGroupInfo.StartDateTime,
                EventEndDateTime = vkGroupInfo.FinishDateTime,
                EventTitle = vkGroupInfo.GroupName,
                EventURL = post.PostURL,
                Attachments = new()
            };

            publicEvents.Add(publicEvent);
        }

        return publicEvents;
    }
}
using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Clients.VkApi.Models;
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
        return await AnalyzeAsync(await GetPostsAsync(groupId, postsCount, cancellationToken), cancellationToken);
    }

    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken)
    {
        return await _vkApiClient.GetGroupInfoAsync(groupId, cancellationToken);
    }

    public async Task<VkPost> GetPostsAsync(string groupId, int maxPosts, CancellationToken cancellationToken)
    {
        return await _vkApiClient.GetPostsAsync(groupId, maxPosts, cancellationToken);
    }


    private async Task<List<PublicEvent>> AnalyzeAsync(VkPost vkPost, CancellationToken cancellationToken)
    {
        VkGroupInfo vkGroupInfo;
        List<PublicEvent> publicEvents = new();

        foreach (var post in vkPost.Posts.Where(p => p.PostAttachment.Any(t => t.Type == "event")))
        {
            vkGroupInfo = await GetGroupInfoAsync(post.PostAttachment.FirstOrDefault(p => p.Type == "event").Event.Id.ToString(), cancellationToken);

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
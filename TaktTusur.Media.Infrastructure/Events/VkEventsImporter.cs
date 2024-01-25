using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Clients.VkApi.Models;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public class VkEventsImporter : IVkEventsImporter
{
    private VkApiClient _vkApiClient;


    public VkEventsImporter(VkApiClient apiClient)
    {
        _vkApiClient = apiClient;
    }
    
    private async Task<List<PublicEvent>> AnalyzeAsync(VkPosts vkPosts, CancellationToken cancellationToken)
    {
        // VkGroupInfo vkGroupInfo;
        // List<PublicEvent> publicEvents = new();
        //
        // foreach (var post in vkPosts.Posts.Where(p => p.PostAttachment.Any(t => t.Type == "event")))
        // {
        //     vkGroupInfo = await GetGroupInfoAsync(post.PostAttachment.FirstOrDefault(p => p.Type == "event").Event.Id.ToString(), cancellationToken);
        //
        //     PublicEvent publicEvent = new()
        //     {
        //         EventStartDateTime = vkGroupInfo.StartDateTime,
        //         EventEndDateTime = vkGroupInfo.FinishDateTime,
        //         EventTitle = vkGroupInfo.GroupName,
        //         EventURL = post.PostURL,
        //         Attachments = new()
        //     };
        //
        //     publicEvents.Add(publicEvent);
        // }
        throw new NotImplementedException();
        //return publicEvents;
    }

    public Task<List<PublicEvent>> ImportAsync(string vkPublicEventId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
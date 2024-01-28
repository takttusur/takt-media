using TaktTusur.Media.Clients.VkApi;
using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public class VkEventsImporter : IVkEventsImporter
{
    private readonly IVkApiClient _vkApiClient;
    
    public VkEventsImporter(IVkApiClient apiClient)
    {
        _vkApiClient = apiClient;
    }

    /// <summary>
    /// Чтение одного мероприятия.
    /// </summary>
    /// <param name="vkPublicEventId">ID события</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Если передан ID мероприятия, будет возвращет <see cref="PublicEvent"/>, иначе null></returns>
    public async Task<PublicEvent?> ImportAsync(string vkPublicEventId, CancellationToken cancellationToken)
    {
        var group = await _vkApiClient.GetGroupInfoAsync(vkPublicEventId, cancellationToken);
        return group is { StartDateTime: not null } ? new PublicEvent()
        {
            Title = group.Name,
            Start = group.StartDateTime.Value,
            Finish = group.FinishDateTime,
            OriginalUrl = group.URL
        } : null;
    }
}
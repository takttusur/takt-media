using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public interface IVkEventsImporter
{
    public Task<List<PublicEvent>> ImportAsync(string vkGroupId, int count, CancellationToken cancellationToken);
}
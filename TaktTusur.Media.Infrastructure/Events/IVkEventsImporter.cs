using TaktTusur.Media.Domain.Events;

namespace TaktTusur.Media.Infrastructure.Events;

public interface IVkEventsImporter
{
    public Task<PublicEvent?> ImportAsync(string vkPublicEventId, CancellationToken cancellationToken);
}
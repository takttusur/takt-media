using TaktTusur.Media.BackgroundCrawling.Core.Entities;
using TaktTusur.Media.Core.Interfaces;

namespace TaktTusur.Media.Core.News;

public class Article : IIdentifiable, IReplicated
{
    public long Id { get; }
    
    public string Text { get; set; }
    
    public DateTimeOffset LastUpdated { get; set; }
    
    public string? OriginalSource { get; }
    public string? OriginalId { get; }
    public DateTimeOffset? OriginalUpdatedAt { get; }
}
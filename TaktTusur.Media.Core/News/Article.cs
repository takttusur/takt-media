using TaktTusur.Media.BackgroundCrawling.Core.Entities;

namespace TaktTusur.Media.Core.News;

public class Article : IIdentifiable
{
    public long Id { get; }
    
    public string? SourceIdentifier { get; set; }
    
    public string? OriginalReference { get; set; }
    
    public string Text { get; set; }
    
    public DateTimeOffset? OriginalLastUpdated { get; set; }
    
    public DateTimeOffset LastUpdated { get; set; }
}
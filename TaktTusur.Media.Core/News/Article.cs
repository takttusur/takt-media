namespace TaktTusur.Media.Core.News;

public class Article
{
    public string? SourceIdentifier { get; set; }
    public string? OriginalReference { get; set; }
    
    public string Text { get; set; }
    
    public DateTimeOffset? OriginalLastUpdated { get; set; }
    
    public DateTimeOffset LastUpdated { get; set; }
}
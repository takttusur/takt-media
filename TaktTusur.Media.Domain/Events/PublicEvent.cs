
namespace TaktTusur.Media.Domain.Events;

public class PublicEvent
{
    /// <summary>
    /// Дата и время начала события
    /// </summary>
    public DateTimeOffset EventStartDateTime { get; set; }

    /// <summary>
    /// Дата и время окончания события
    /// </summary>
    public DateTimeOffset? EventEndDateTime { get; set; }

    /// <summary>
    /// Название события
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Ссылка на оригинал новости/события
    /// </summary>
    public string? OriginalUrl { get; set; }
}
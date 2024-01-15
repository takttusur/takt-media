using TaktTusur.Media.Domain.Common;

namespace TaktTusur.Media.Domain.Events;

public class PublicEvent
{
    /// <summary>
    /// ¬озвращает и задает дату и врем€ начала событи€.
    /// </summary>
    public int EventStartDateTime { get; set; }

    /// <summary>
    /// ¬озвращает и задает дату и врем€ завершени€ событи€.
    /// </summary>
    public int EventEndDateTime { get; set; }

    /// <summary>
    /// ¬озвращает и задает описание событи€.
    /// </summary>
    public string EventTitle { get; set; }

    /// <summary>
    /// ¬озвращает и задает медиаресурсы.
    /// </summary>
    public List<Attachment> Attachments { get; set; }

    /// <summary>
    /// ¬озвращает и задает URL-ссылку на данное событие.
    /// </summary>
    public string EventURL { get; set; }
}
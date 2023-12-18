using TaktTusur.Media.Domain.Common;

namespace TaktTusur.Media.Domain.Events;

public class PublicEvent
{
    /// <summary>
    /// Возвращает и задает дату и время начала события.
    /// </summary>
    public int EventStartDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает дату и время завершения события.
    /// </summary>
    public int EventEndDateTime { get; set; }

    /// <summary>
    /// Возвращает и задает описание события
    /// </summary>
    public string EventTitle { get; set; }

    /// <summary>
    /// Возвращает и задает массив объектов, соответствующих медиаресурсам, прикреплённым к событию.
    /// </summary>
    public List<Attachment> EventAttachments { get; set; } = new List<Attachment>();

    /// <summary>
    /// Возвращает и задает URL-ссылку на данное событие.
    /// </summary>
    public string EventURL { get; set; }
}
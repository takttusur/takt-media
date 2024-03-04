using TaktTusur.Media.Clients.VkApi.Models.Enums;
using TaktTusur.Media.Clients.VkApi.Models.Common;

namespace TaktTusur.Media.Clients.VkApi.Models
{
    /// <summary>
    /// Запись на стене сообщества.
    /// </summary>
    public class WallPost
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, на которой размещена запись.
        /// </summary>
        public long SourceId { get; set; }

        /// <summary>
        /// Время публикации записи.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Источник материала записи.
        /// </summary>
        public Copyrights PostCopyrightNotes { get; set; } = new Copyrights();

        /// <summary>
        /// Тип записи.
        /// </summary>
        public WallPostTypes PostType { get; set; }

        /// <summary>
        /// Массив объектов, соответствующих медиаресурсам, прикреплённым к записи.
        /// </summary>
        public List<Attachment> PostAttachment { get; set; } = new List<Attachment>();

        /// <summary>
        /// Текст записи.
        /// </summary>
        public string PostText { get; set; }

        /// <summary>
        /// URL записи.
        /// </summary>
        public string PostURL { get; set; }

        /// <summary>
        /// Коллекция записей (если исходную запись репостили).
        /// </summary>
        public List<WallPost> InnerPosts { get; set; } = new List<WallPost>();
    }
}
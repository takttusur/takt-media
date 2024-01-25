using TaktTusur.Media.Clients.VkApi.WallByIdResponse;

namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class WallPost
    {
        /// <summary>
        /// Id поста.
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Id источника поста(группа, пользователь, мероприятие)
        /// </summary>
        public long SourceId { get; set; }

        /// <summary>
        /// Возвращает и задает дату и время создания записи.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Возвращает и задает источник материала записи.
        /// </summary>
        public Copyrights PostCopyrightNotes { get; set; } = new Copyrights();

        /// <summary>
        /// Возвращает и задает тип записи.
        /// </summary>
        public WallPostTypes PostType { get; set; }

        /// <summary>
        /// Возвращает и задает массив объектов, соответствующих медиаресурсам, прикреплённым к записи.
        /// </summary>
        public List<Attachment> PostAttachment { get; set; } = new List<Attachment>();

        /// <summary>
        /// Возвращает и задает текст записи.
        /// </summary>
        public string PostText { get; set; }

        /// <summary>
        /// Возвращает и задает URL-ссылку на данную запись.
        /// </summary>
        public string PostURL { get; set; }

        /// <summary>
        /// Если запись является репостом, то тут будет цепочка репостов.
        /// </summary>
        public List<WallPost> InnerPosts { get; set; } = new List<WallPost>();
    }
}
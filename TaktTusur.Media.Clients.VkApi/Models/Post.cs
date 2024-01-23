using TaktTusur.Media.Clients.VkApi.WallByIdResponse;

namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class Post
    {
        /// <summary>
        /// Возвращает и задает Id записи.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Возвращает и задает дату и время создания записи.
        /// </summary>
        public int PostDataTimeOfCreation { get; set; }

        /// <summary>
        /// Возвращает и задает источник материала записи.
        /// </summary>
        public Copyrights PostCopyrightNotes { get; set; } = new Copyrights();

        /// <summary>
        /// Возвращает и задает тип записи.
        /// </summary>
        public string PostType { get; set; }

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
    }
}
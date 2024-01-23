namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class VkPost
    {
        /// <summary>
        /// Список запрошенных записей
        /// </summary>
        public List<Post> Posts { get; set; } = new List<Post>();

        /// <summary>
        /// Общее количество записей в группе/событии, который подходят под фильтр
        /// </summary>
        public int Count { get; set; }
    }
}
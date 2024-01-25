namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class VkPosts
    {
        /// <summary>
        /// Список запрошенных записей
        /// </summary>
        public List<WallPost> Posts { get; set; } = new List<WallPost>();

        /// <summary>
        /// Общее количество записей в группе/событии, который подходят под фильтр
        /// </summary>
        public int Count { get; set; }
    }
}
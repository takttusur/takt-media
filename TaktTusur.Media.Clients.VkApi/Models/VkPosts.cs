namespace TaktTusur.Media.Clients.VkApi.Models
{
    public class VkPosts
    {
        /// <summary>
        /// Полученные записи.
        /// </summary>
        public List<WallPost> Posts { get; set; } = new List<WallPost>();

        /// <summary>
        /// Количество записей сообщества/события, подходящих под фильтр.
        /// </summary>
        public int Count { get; set; }
    }
}
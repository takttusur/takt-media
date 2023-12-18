namespace TaktTusur.Media.Clients.VkApi
{
    public class VkPost
    {
        /// <summary>
        /// Возвращает и задает список записей сообщества.
        /// </summary>
        public List<Post> Posts { get; set; } = new List<Post>();

        /// <summary>
        /// Возвращает и задает количество получаемых записей.
        /// </summary>
        public int Count { get; set; }
    }
}
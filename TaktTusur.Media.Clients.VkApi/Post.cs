using TaktTusur.Media.Clients.VkApi.WallByIdResponse;

namespace TaktTusur.Media.Clients.VkApi
{
    public class Post
    {
        public int PostDataTimeOfCreation { get; set; }
        public int PostId { get; set; }
        public Copyrights PostCopyrightNotes { get; set; } = new Copyrights();
        public string PostType { get; set; }
        public string PostText { get; set; }
        public string PostURL { get; set; }
        public List<Attachment> PostAttachment { get; set; } = new List<Attachment>();
    }
}

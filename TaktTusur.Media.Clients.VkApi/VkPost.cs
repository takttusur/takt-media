using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi
{
    public class VkPost
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public int Count { get; set; }
    }
    public class Post 
    {
        public int PostDataTimeOfCreation { get; set; }
        public Copyrights PostCopyrightNotes { get; set; } = new Copyrights ();   
        public string PostType { get; set; }
        public string PostText { get; set; }
        public string PostURL { get; set; }
        public List<Attachment> PostAttachment { get; set; } = new List<Attachment>();
    }
}

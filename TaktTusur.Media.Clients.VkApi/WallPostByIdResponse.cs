using Elasticsearch.Net;
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi
{

    public class WallResponse
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class WallPostByIdResponse
    {
        public WallResponse Response { get; set; }
        public Error PostsError { get; set; }
    }

    public class Album
    {
        public int created { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public int size { get; set; }
        public string title { get; set; }
        public int updated { get; set; }
        public string description { get; set; }
        public Thumb thumb { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Doc doc { get; set; }
        public Link link { get; set; }
        public Photo photo { get; set; }
        public Album album { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
    }

    public class CopyHistory
    {
        public string inner_type { get; set; }
        public string type { get; set; }
        public List<Attachment> attachments { get; set; }
        public int date { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public PostSource post_source { get; set; }
        public string post_type { get; set; }
        public int signer_id { get; set; }
        public string text { get; set; }
    }

    public class Doc
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int is_unsafe { get; set; }
        public string access_key { get; set; }
    }

    public class Donut
    {
        public bool is_donut { get; set; }
    }

    public class Item
    {
        public string inner_type { get; set; }
        public List<CopyHistory> copy_history { get; set; }
        public Donut donut { get; set; }
        public Comments comments { get; set; }
        public int marked_as_ads { get; set; }
        public double short_text_rate { get; set; }
        public string hash { get; set; }
        public string type { get; set; }
        public List<Attachment> attachments { get; set; }
        public int date { get; set; }
        public int edited { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public Likes likes { get; set; }
        public int owner_id { get; set; }
        public string post_type { get; set; }
        public Reposts reposts { get; set; }
        public string text { get; set; }
        public Views views { get; set; }
        public int? carousel_offset { get; set; }
    }

    public class Likes
    {
        public int can_like { get; set; }
        public int count { get; set; }
        public int user_likes { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }

    public class Photo
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public List<Size> sizes { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public string web_view_token { get; set; }
        public bool has_tags { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
    }

    public class Size
    {
        public int height { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public string url { get; set; }
    }

    public class Thumb
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public List<Size> sizes { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public string web_view_token { get; set; }
        public bool has_tags { get; set; }
    }

    public class Views
    {
        public int count { get; set; }
    }

}

using Elasticsearch.Net;
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi
{

    public class WallResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }

    public class WallPostByIdResponse
    {
        [JsonPropertyName("response")]
        public WallResponse Response { get; set; }

        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class Album
    {
        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("update")]
        public int Updated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumb")]
        public Thumb Thumb { get; set; } = new Thumb();
    }

    public class Attachment
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("doc")]
        public Doc Doc { get; set; } = new Doc();

        [JsonPropertyName("link")]
        public Link Link { get; set; } = new Link();

        [JsonPropertyName("photo")]
        public Photo Photo { get; set; } = new Photo();

        [JsonPropertyName("album")]
        public Album Album { get; set; } = new Album(); 

         [JsonPropertyName("video")]
         public Video Video { get; set; } = new Video();    
    }



    public class Comment
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class CopyHistory
    {
        [JsonPropertyName("inner_type")]
        public string InnerType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; } = new List<Attachment> ();

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("from_id")]
        public int FromId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("post_source")]
        public PostSource PostSource { get; set; } = new PostSource();

        [JsonPropertyName("post_type")]
        public string PostType { get; set; }

        [JsonPropertyName("singer_id")]
        public int SignerId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Doc
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("ext")]
        public string Ext { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("is_unsafe")]
        public int IsUnsafe { get; set; }

        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }

    public class Donut
    {
        [JsonPropertyName("is_donut")]
        public bool IsDonut { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("inner_type")]
        public string InnerType { get; set; }

        [JsonPropertyName("copy_history")]
        public List<CopyHistory> CopyHistory { get; set; } = new List<CopyHistory> ();

        [JsonPropertyName("donut")]
        public Donut Donut { get; set; } = new Donut ();

        [JsonPropertyName("comments")]
        public Comment Comments { get; set; } = new Comment ();

        [JsonPropertyName("marked_as_ads")]
        public int MarkedAsAds { get; set; }

        [JsonPropertyName("short_text_rate")]
        public double ShortTextRate { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("edited")]
        public int Edited { get; set; }

        [JsonPropertyName("from_id")]
        public int FromId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("likes")]
        public Likes Likes { get; set; } = new Likes ();

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("post_type")]
        public string PostType { get; set; }

        [JsonPropertyName("reposts")]
        public Reposts Reposts { get; set; } = new Reposts (); 

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("views")]
        public Views Views { get; set; } = new Views();

        [JsonPropertyName("carousel_offset")]
        public int? CarouselOffset { get; set; }

        [JsonPropertyName("copyright")]
        public Copyrights Copyrights { get; set; } = new Copyrights ();
    }

    public class Copyrights
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

    } 

    public class Likes
    {
        [JsonPropertyName("can_like")]
        public int CanLike { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("user_likes")]
        public int UserLikes { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class Photo
    {
        [JsonPropertyName("album_id")]
        public int? AlbumId { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; } = new List<Size> ();

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("web_view_token")]
        public string WebViewToken { get; set; }

        [JsonPropertyName("has_tags")]
        public bool HasTags { get; set; }
    }

    public class Video
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("image")]
        public List<Image> Images { get; set; } = new List<Image> ();

        [JsonPropertyName("first_frame")]
        public List<FirstFrame> FirstFrames { get; set; } = new List<FirstFrame> ();

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("adding_date")]
        public int AddingDate { get; set; }

        [JsonPropertyName("views")]
        public int Views { get; set; }

        [JsonPropertyName("response_type")]
        public string ResponseType { get; set; }

        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }

        [JsonPropertyName("can_like")]
        public int CanLike { get; set; }

        [JsonPropertyName("can_repost")]
        public int CanRepost { get; set; }

        [JsonPropertyName("can_subscribe")]
        public int CanSubscribe { get; set; }

        [JsonPropertyName("can_add_to_faves")]
        public int CanAddToFaves { get; set; }

        [JsonPropertyName("can_add")]
        public int CanAdd { get; set; }

        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("track_code")]
        public string TrackCode { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        [JsonPropertyName("can_dislike")]
        public int CanDislike { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("with_padding")]
        public int WithPadding { get; set; }
    }

    public class FirstFrame
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class PostSource
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Reposts
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class Size
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Thumb
    {
        [JsonPropertyName("album_id")]
        public int AlbumId { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }

        [JsonPropertyName("sizes")]
        public List<Size> Sizes { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("web_view_token")]
        public string WebViewToken { get; set; }

        [JsonPropertyName("has_tags")]
        public bool HasTags { get; set; }
    }

    public class Views
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

}

using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Item
    {
        [JsonPropertyName("inner_type")]
        public string InnerType { get; set; }

        [JsonPropertyName("copy_history")]
        public List<CopyHistory> CopyHistory { get; set; } = new List<CopyHistory>();

        [JsonPropertyName("donut")]
        public Donut Donut { get; set; } = new Donut();

        [JsonPropertyName("comments")]
        public Comment Comments { get; set; } = new Comment();

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
        public Likes Likes { get; set; } = new Likes();

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("post_type")]
        public string PostType { get; set; }

        [JsonPropertyName("reposts")]
        public Reposts Reposts { get; set; } = new Reposts();

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("views")]
        public Views Views { get; set; } = new Views();

        [JsonPropertyName("carousel_offset")]
        public int? CarouselOffset { get; set; }

        [JsonPropertyName("copyright")]
        public Copyrights Copyrights { get; set; } = new Copyrights();
    }
}

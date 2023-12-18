using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class CopyHistory
    {
        [JsonPropertyName("inner_type")]
        public string InnerType { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

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
}
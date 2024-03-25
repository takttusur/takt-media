using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Thumb
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
        public List<Size> Sizes { get; set; } = new();

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("web_view_token")]
        public string WebViewToken { get; set; }

        [JsonPropertyName("has_tags")]
        public bool HasTags { get; set; }
    }
}
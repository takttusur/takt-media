using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Attachment
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

        [JsonPropertyName("event")]
        public VkEvent Event { get; set; } = new VkEvent();
    }
}

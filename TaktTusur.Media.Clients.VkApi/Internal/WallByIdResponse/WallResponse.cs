using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class WallResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("items")]
        public List<PostDto> Items { get; set; }

        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}
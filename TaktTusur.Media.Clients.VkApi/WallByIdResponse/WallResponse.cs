using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class WallResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("items")]
        public List<PostDto> Items { get; set; }

        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}
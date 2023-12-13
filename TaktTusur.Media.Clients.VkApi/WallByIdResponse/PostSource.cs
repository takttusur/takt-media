using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class PostSource
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

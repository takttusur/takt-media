using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Reposts
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}

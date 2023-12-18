using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Views
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
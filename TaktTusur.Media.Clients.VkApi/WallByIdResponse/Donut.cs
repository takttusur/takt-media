using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Donut
    {
        [JsonPropertyName("is_donut")]
        public bool IsDonut { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Donut
    {
        [JsonPropertyName("is_donut")]
        public bool IsDonut { get; set; }
    }
}
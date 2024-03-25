using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class PostSource
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
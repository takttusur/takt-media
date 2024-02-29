using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Reposts
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
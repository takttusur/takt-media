using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Views
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
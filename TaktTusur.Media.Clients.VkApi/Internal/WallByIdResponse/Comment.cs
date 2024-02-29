using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Comment
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Group
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
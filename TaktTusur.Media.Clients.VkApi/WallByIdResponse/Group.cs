using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Group
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}

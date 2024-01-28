using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Album
    {
        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("update")]
        public int Updated { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumb")]
        public Thumb Thumb { get; set; } = new Thumb();
    }
}
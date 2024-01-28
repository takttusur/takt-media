using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Likes
    {
        [JsonPropertyName("can_like")]
        public int CanLike { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("user_likes")]
        public int UserLikes { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class WallPostByIdResponse
    {
        [JsonPropertyName("response")]
        public WallResponse? Response { get; set; }

        [JsonPropertyName("error")]
        public Error? WallPostError { get; set; }
    }
}
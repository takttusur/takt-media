using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class WallPostByIdResponse
    {
        [JsonPropertyName("response")]
        public WallResponse? Response { get; set; }

        [JsonPropertyName("error")]
        public Error? WallPostError { get; set; }
    }
}
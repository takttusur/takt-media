using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.WallByIdResponse
{
    public class Error
    {
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_msg")]
        public string ErrorMessage { get; set; }
    }
}
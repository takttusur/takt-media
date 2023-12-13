using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.GroupInfoResponse
{
    public class Response
    {
        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}

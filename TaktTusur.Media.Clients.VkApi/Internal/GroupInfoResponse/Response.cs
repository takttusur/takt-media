using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.GroupInfoResponse
{
    internal class Response
    {
        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}
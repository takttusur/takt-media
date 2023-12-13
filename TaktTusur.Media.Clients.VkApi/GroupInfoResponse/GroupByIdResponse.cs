using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.GroupInfoResponse;

public class GroupByIdResponse
{
    [JsonPropertyName("response")]
    public Response Response { get; set; }

    [JsonPropertyName("error")]
    public Error GroupInfoError { get; set; }
}




using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.GroupInfoResponse;

internal class GroupByIdResponse
{
    [JsonPropertyName("response")]
    public Response Response { get; set; }

    [JsonPropertyName("error")]
    public Error GroupInfoError { get; set; }
}
using RestEase;
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi;

public class GroupByIdRequest
{
    //TODO: сделать в camel, как я понял [JsonPropertyName("...")] работает для присылаемого JSON, а как сделать для запроса я пока не нашел 

    //[JsonPropertyName("group_id")]
    /* [RequestAttribute(Name = "group_id")]
     public string GroupId { get; set; }

     //[JsonPropertyName("access_token")]
     public string AccessToken { get; set; }

     //[JsonPropertyName("v")]
     public string Version { get; set; }*/
    public string group_id { get; set; }
    public string access_token { get; set; }
    public string v { get; set; }
}
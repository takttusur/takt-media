using RestSharp;
using RestSharp.Authenticators;

namespace TaktTusur.Media.Clients.VkApi;

public class VkApiClient: IVkApiClient
{
    private readonly VkApiOptions _options;

    public VkApiClient(VkApiOptions options)
    {
        _options = options;
    }

    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken)
    {
        
        var info = new VkGroupInfo();
        var options = new RestClientOptions("https://api.vk.com/method") {
            ThrowOnAnyError = true, FailOnDeserializationError = true, ThrowOnDeserializationError = true
        };
        var client = new RestClient(options);
        var request = new GroupByIdRequest()
        {
            v = "5.154", access_token = _options.Key, group_id = groupId
        };
// The cancellation token comes from the caller. You can still make a call without it.
        var result = await client.GetJsonAsync<GroupByIdResponse>("groups.getById",request, cancellationToken);
        info.GroupName = result.response.groups[0].name;
        return info;
    }
}
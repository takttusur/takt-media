using RestSharp;

namespace TaktTusur.Media.Clients.VkApi;
public class VkApiClient: IVkApiClient
{
   
    private readonly VkApiOptions _options;
    private readonly RestClientOptions restClientOptions;
    private readonly GroupByIdRequest GroupRequest;
    private readonly WallByIdRequest WallRequest;

    /*public VkApiClient(VkApiOptions options)
    {
        _options = options;
    }*/
    public VkApiClient(VkApiOptions options, string groupId)
    {
        _options=  options;
        restClientOptions = new RestClientOptions("https://api.vk.com/method")
        {
            ThrowOnAnyError = true, FailOnDeserializationError = true, ThrowOnDeserializationError = true
        };

        GroupRequest = new GroupByIdRequest()
        {

            // TODO: вынести строки в КОНСТАНТННЫЕ переменные КЭМЛ
            //Version = "5.154", AccessToken = _options.Key, GroupId = groupId
            v = "5.154", access_token = _options.Key, group_id = groupId
        };
        
        WallRequest = new WallByIdRequest()
        {

            // TODO: вынести строки в КОНСТАНТННЫЕ переменные КЭМЛ
            //Version = "5.154", AccessToken = _options.Key, GroupId = groupId
            v = "5.154",
            access_token = _options.Key,
            domain = groupId,
            count = 5,
            extended = 0
        };

    }
        
    public async Task<VkGroupInfo> GetGroupInfoAsync(CancellationToken cancellationToken)
    {
        
        var info = new VkGroupInfo();
        var client = new RestClient(restClientOptions);
        var result = await client.GetJsonAsync<GroupByIdResponse>("groups.getById", GroupRequest, cancellationToken);
        
        //TODO:проверка на ошибку вк и на отсутствие групп
        if (result.GroupInfoError == null) 
        { 
            info.GroupName = result.Response.Groups[0].Name; 
        }
        else Console.WriteLine("ошибка в блоке информации о группе");
        //info.GroupName = result.Response.Groups[0].Name;
        return info;
    }

     public async Task<VkPost> GetPostsAsync(CancellationToken cancellationToken)
     {
        var tr = new VkPost();
        var client = new RestClient(restClientOptions);
        var PostResult = await client.GetJsonAsync<WallPostByIdResponse>("wall.get", WallRequest, cancellationToken);
        
        //TODO:проверка на ошибку вк и на отсутствие групп 
        if (PostResult.PostsError == null) 
        { 
            tr.Count = PostResult.Response.count;
        }
        else Console.WriteLine("ошибка в блоке посты");

        return tr;


    }

}
using RestSharp;
using TaktTusur.Media.Clients.VkApi.WallByIdResponse;
using TaktTusur.Media.Clients.VkApi.GroupInfoResponse;

namespace TaktTusur.Media.Clients.VkApi;
public class VkApiClient: IVkApiClient
{
   
    private readonly VkApiOptions _options;
    private readonly RestClientOptions restClientOptions;
    private readonly GroupByIdRequest GroupRequest;
    private readonly WallByIdRequest WallRequest;

    // Создание request(запросов) для группы(GroupRequest) и для стены группы(WallRequest)
    public VkApiClient(VkApiOptions options, string groupId)
    {
        _options=  options;
        restClientOptions = new RestClientOptions("https://api.vk.com/method")
        {
            ThrowOnAnyError = true, FailOnDeserializationError = true, ThrowOnDeserializationError = true
        };

        GroupRequest = new GroupByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key, 
            GroupId = groupId
        };
        
        WallRequest = new WallByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key,
            Domain = groupId,
            Count = 5,
            Extended = 0
        };

    }
    
    // Метод запрашивает информацию о группе вк по ее id, а после обрабатывает ответ и заносит данные в info
    public async Task<VkGroupInfo> GetGroupInfoAsync(CancellationToken cancellationToken)
    {
        
        var info = new VkGroupInfo();
        var client = new RestClient(restClientOptions);
        var result = await client.GetJsonAsync<GroupByIdResponse>("groups.getById", GroupRequest, cancellationToken);
        
        if (result.GroupInfoError == null) 
        { 
            info.GroupId = result.Response.Groups[0].Id;
            info.GroupName = result.Response.Groups[0].Name;
            info.GroupScreenName = result.Response.Groups[0].ScreenName;
            info.GroupType = result.Response.Groups[0].Type;
            info.GroupPhoto50 = result.Response.Groups[0].Photo50;
            info.GroupPhoto100 = result.Response.Groups[0].Photo100;
            info.GroupPhoto200 = result.Response.Groups[0].Photo200;
            info.GroupIsClosed = result.Response.Groups[0].IsClosed;
        }
        else throw new Exception("ошибка в блоке информации о группе");
        
        return info;
    }

    // Метод запрашивает информацию о постах со стены группы вк по id группы,
    // в количестве указанном в Сount в VkApiClient в WallRequest,
    // а после обрабатывает ответ и заносит данные в TestResult
    public async Task<VkPost> GetPostsAsync(CancellationToken cancellationToken)
     {
        //чтобы вытащить id группы
        var Group = new VkApiClient(_options, WallRequest.Domain);
        var GroupId = Group.GetGroupInfoAsync(CancellationToken.None);
        
        var TestResult = new VkPost();
        var Client = new RestClient(restClientOptions);
        var PostResult = await Client.GetJsonAsync<WallPostByIdResponse>("wall.get", WallRequest, cancellationToken);
        
        if (PostResult.Error == null)
        {
            for(int i = 0; i < PostResult.Response.Items.Count; i++)
            {
                var WallPost = new Post
                {
                    PostText = PostResult.Response.Items[i].Text,
                    PostDataTimeOfCreation = PostResult.Response.Items[i].Date,
                    PostType = PostResult.Response.Items[i].Type,
                    PostURL = "https://vk.com/public" + GroupId.Result.GroupId + "?w=wall-" + GroupId.Result.GroupId + "_" + PostResult.Response.Items[i].Id
                };

                    WallPost.PostCopyrightNotes.Id = PostResult.Response.Items[i].Copyrights.Id;
                    WallPost.PostCopyrightNotes.Link = PostResult.Response.Items[i].Copyrights.Link;
                    WallPost.PostCopyrightNotes.Name = PostResult.Response.Items[i].Copyrights.Name;
                    WallPost.PostCopyrightNotes.Type = PostResult.Response.Items[i].Copyrights.Type;

                for (int j = 0; j < PostResult.Response.Items[i].Attachments.Count; j++)
                {
                    var Attachments = new Attachment
                    {
                        Type = PostResult.Response.Items[i].Attachments[j].Type,
                    };

                    if (Attachments.Type == "photo")
                    {
                        Attachments.Photo.AlbumId = PostResult.Response.Items[i].Attachments[j].Photo.AlbumId;
                        Attachments.Photo.Date = PostResult.Response.Items[i].Attachments[j].Photo.Date;
                        Attachments.Photo.Id = PostResult.Response.Items[i].Attachments[j].Photo.Id;
                        Attachments.Photo.OwnerId = PostResult.Response.Items[i].Attachments[j].Photo.OwnerId;
                        Attachments.Photo.AccessKey = PostResult.Response.Items[i].Attachments[j].Photo.AccessKey;
                        Attachments.Photo.Text = PostResult.Response.Items[i].Attachments[j].Photo.Text;
                        Attachments.Photo.UserId = PostResult.Response.Items[i].Attachments[j].Photo.UserId;

                        for (int k = 0; k < PostResult.Response.Items[i].Attachments[j].Photo.Sizes.Count; k++)
                        {
                            var Size = new Size
                            {
                                Height = PostResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Height,
                                Width = PostResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Width,
                                Type = PostResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Type,
                                Url = PostResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Url,
                            };

                            Attachments.Photo.Sizes.Add(Size);
                        }
                    }

                    else if (Attachments.Type == "link")
                    {
                        Attachments.Link.Url = PostResult.Response.Items[i].Attachments[j].Link.Url;
                        Attachments.Link.Caption = PostResult.Response.Items[i].Attachments[j].Link.Caption;
                        Attachments.Link.Description = PostResult.Response.Items[i].Attachments[j].Link.Description;
                        Attachments.Link.Title = PostResult.Response.Items[i].Attachments[j].Link.Title;
                    }
                    else if (Attachments.Type == "doc")
                    {
                        Attachments.Doc.Id = PostResult.Response.Items[i].Attachments[j].Doc.Id;
                        Attachments.Doc.OwnerId = PostResult.Response.Items[i].Attachments[j].Doc.OwnerId;
                        Attachments.Doc.Title = PostResult.Response.Items[i].Attachments[j].Doc.Title;
                        Attachments.Doc.Size = PostResult.Response.Items[i].Attachments[j].Doc.Size;
                        Attachments.Doc.Ext = PostResult.Response.Items[i].Attachments[j].Doc.Ext;
                        Attachments.Doc.Date = PostResult.Response.Items[i].Attachments[j].Doc.Date;
                        Attachments.Doc.Type = PostResult.Response.Items[i].Attachments[j].Doc.Type;
                        Attachments.Doc.Url = PostResult.Response.Items[i].Attachments[j].Doc.Url;
                        Attachments.Doc.IsUnsafe = PostResult.Response.Items[i].Attachments[j].Doc.IsUnsafe;
                        Attachments.Doc.AccessKey = PostResult.Response.Items[i].Attachments[j].Doc.AccessKey;
                    }
                    else if (Attachments.Type == "album")
                    { 
                        Attachments.Album.Created = PostResult.Response.Items[i].Attachments[j].Album.Created;
                        Attachments.Album.Id = PostResult.Response.Items[i].Attachments[j].Album.Id;
                        Attachments.Album.OwnerId = PostResult.Response.Items[i].Attachments[j].Album.OwnerId;
                        Attachments.Album.Size = PostResult.Response.Items[i].Attachments[j].Album.Size;
                        Attachments.Album.Title = PostResult.Response.Items[i].Attachments[j].Album.Title;
                        Attachments.Album.Updated = PostResult.Response.Items[i].Attachments[j].Album.Updated;
                        Attachments.Album.Description = PostResult.Response.Items[i].Attachments[j].Album.Description;
                        
                        Attachments.Album.Thumb.AlbumId = PostResult.Response.Items[i].Attachments[j].Album.Thumb.AlbumId;
                        Attachments.Album.Thumb.Date = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Date;
                        Attachments.Album.Thumb.Id = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Id;
                        Attachments.Album.Thumb.OwnerId = PostResult.Response.Items[i].Attachments[j].Album.Thumb.OwnerId;
                        Attachments.Album.Thumb.AccessKey = PostResult.Response.Items[i].Attachments[j].Album.Thumb.AccessKey;
                        Attachments.Album.Thumb.Text = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Text;
                        Attachments.Album.Thumb.UserId = PostResult.Response.Items[i].Attachments[j].Album.Thumb.UserId;
                        Attachments.Album.Thumb.WebViewToken = PostResult.Response.Items[i].Attachments[j].Album.Thumb.WebViewToken;
                        Attachments.Album.Thumb.HasTags = PostResult.Response.Items[i].Attachments[j].Album.Thumb.HasTags;

                        for (int k = 0; k < PostResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes.Count; k++)
                        {
                            var Size = new Size
                            {
                                Height = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Height,
                                Width = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Width,
                                Type = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Type,
                                Url = PostResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Url,
                            };

                            Attachments.Album.Thumb.Sizes.Add(Size);
                        }
                    }
                    else if (Attachments.Type == "video")
                    {
                        Attachments.Video.Id = PostResult.Response.Items[i].Attachments[j].Video.Id;
                        Attachments.Video.OwnerId = PostResult.Response.Items[i].Attachments[j].Video.OwnerId;
                        Attachments.Video.Title = PostResult.Response.Items[i].Attachments[j].Video.Title;
                        Attachments.Video.Description = PostResult.Response.Items[i].Attachments[j].Video.Description;
                        Attachments.Video.Duration = PostResult.Response.Items[i].Attachments[j].Video.Duration;
                        Attachments.Video.Date = PostResult.Response.Items[i].Attachments[j].Video.Date;
                        Attachments.Video.AddingDate = PostResult.Response.Items[i].Attachments[j].Video.AddingDate;
                        Attachments.Video.Views = PostResult.Response.Items[i].Attachments[j].Video.Views;
                        Attachments.Video.ResponseType = PostResult.Response.Items[i].Attachments[j].Video.ResponseType;
                        Attachments.Video.AccessKey = PostResult.Response.Items[i].Attachments[j].Video.AccessKey;
                        Attachments.Video.CanLike = PostResult.Response.Items[i].Attachments[j].Video.CanLike;
                        Attachments.Video.CanRepost = PostResult.Response.Items[i].Attachments[j].Video.CanRepost;
                        Attachments.Video.CanSubscribe = PostResult.Response.Items[i].Attachments[j].Video.CanSubscribe;
                        Attachments.Video.CanAddToFaves = PostResult.Response.Items[i].Attachments[j].Video.CanAddToFaves;
                        Attachments.Video.CanAdd = PostResult.Response.Items[i].Attachments[j].Video.CanAdd;
                        Attachments.Video.Comments = PostResult.Response.Items[i].Attachments[j].Video.Comments;
                        Attachments.Video.TrackCode = PostResult.Response.Items[i].Attachments[j].Video.TrackCode;
                        Attachments.Video.Type = PostResult.Response.Items[i].Attachments[j].Video.Type;
                        Attachments.Video.Platform = PostResult.Response.Items[i].Attachments[j].Video.Platform;
                        Attachments.Video.CanDislike = PostResult.Response.Items[i].Attachments[j].Video.CanDislike;

                        for (int k = 0; k < PostResult.Response.Items[i].Attachments[j].Video.Images.Count; k++)
                        {
                            var Image = new Image
                            {
                                Height = PostResult.Response.Items[i].Attachments[j].Video.Images[k].Height,
                                Width = PostResult.Response.Items[i].Attachments[j].Video.Images[k].Width,
                                WithPadding = PostResult.Response.Items[i].Attachments[j].Video.Images[k].WithPadding,
                                Url = PostResult.Response.Items[i].Attachments[j].Video.Images[k].Url,
                            };

                            Attachments.Video.Images.Add(Image);
                        }

                        for (int k = 0; k < PostResult.Response.Items[i].Attachments[j].Video.FirstFrames.Count; k++)
                        {
                            var FirstFrame = new FirstFrame
                            {
                                Height = PostResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Height,
                                Width = PostResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Width,
                                Url = PostResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Url,
                            };

                            Attachments.Video.FirstFrames.Add(FirstFrame);
                        }
                    }
                    
                    WallPost.PostAttachment.Add(Attachments);

                }

                TestResult.Posts.Add(WallPost);
                TestResult.Count = PostResult.Response.Count;
            }
        }
        else 
        {
            TestResult.Count = 10;
            throw new Exception("ошибка в блоке посты");
        }

        return TestResult;


    }

}
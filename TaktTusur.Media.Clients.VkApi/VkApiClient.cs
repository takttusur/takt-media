using RestSharp;
using TaktTusur.Media.Clients.VkApi.GroupInfoResponse;
using TaktTusur.Media.Clients.VkApi.WallByIdResponse;

namespace TaktTusur.Media.Clients.VkApi;

public class VkApiClient : IVkApiClient
{
    private readonly VkApiOptions _options;
    private readonly RestClientOptions _restClientOptions;

    // Создание request(запросов) для группы(GroupRequest) и для стены группы(WallRequest)
    public VkApiClient(VkApiOptions options)
    {
        _options = options;
        _restClientOptions = new RestClientOptions("https://api.vk.com/method")
        {
            ThrowOnAnyError = true,
            FailOnDeserializationError = true,
            ThrowOnDeserializationError = true
        };
    }

    // Метод запрашивает информацию о группе вк по ее id, а после обрабатывает ответ и заносит данные в info
    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken, List<string> fields = null)
    {
        var groupRequest = new GroupByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key,
            GroupId = groupId,
            Fields = fields
        };

        var info = new VkGroupInfo();
        var client = new RestClient(_restClientOptions);
        var result = await client.GetJsonAsync<GroupByIdResponse>("groups.getById", groupRequest, cancellationToken);

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

            if (info.GroupType == "event")
            {
                info.StartDateTime = result.Response.Groups[0].StartDate;
                info.FinishDateTime = result.Response.Groups[0].FinishDate;
            }
        }
        else throw new VkApiException(result.GroupInfoError.ErrorCode, result.GroupInfoError.ErrorMessage);

        return info;
    }

    // Метод запрашивает информацию о постах со стены группы вк по id группы,
    // в количестве указанном в Сount в VkApiClient в WallRequest,
    // а после обрабатывает ответ и заносит данные в TestResult
    public async Task<VkPost> GetPostsAsync(string groupId, int count, CancellationToken cancellationToken)
    {
        var wallRequest = new WallByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key,
            Domain = groupId,
            Count = count,
            Extended = 1
        };

        var testResult = new VkPost();
        var client = new RestClient(_restClientOptions);
        var postResult = await client.GetJsonAsync<WallPostByIdResponse>("wall.get", wallRequest, cancellationToken);

        if (postResult.WallPostError == null)
        {
            for (int i = 0; i < postResult.Response.Items.Count; i++)
            {
                var wallPost = new Post
                {
                    PostId = postResult.Response.Groups[0].Id,
                    PostText = postResult.Response.Items[i].Text,
                    PostDataTimeOfCreation = postResult.Response.Items[i].Date,
                    PostType = postResult.Response.Items[i].Type,
                };
                wallPost.PostURL = "https://vk.com/public" + wallPost.PostId + "?w=wall-" + wallPost.PostId + "_" + postResult.Response.Items[i].Id;

                wallPost.PostCopyrightNotes.Id = postResult.Response.Items[i].Copyrights.Id;
                wallPost.PostCopyrightNotes.Link = postResult.Response.Items[i].Copyrights.Link;
                wallPost.PostCopyrightNotes.Name = postResult.Response.Items[i].Copyrights.Name;
                wallPost.PostCopyrightNotes.Type = postResult.Response.Items[i].Copyrights.Type;

                for (int j = 0; j < postResult.Response.Items[i].Attachments.Count; j++)
                {
                    var attachments = new Attachment
                    {
                        Type = postResult.Response.Items[i].Attachments[j].Type,
                    };

                    if (attachments.Type == "photo")
                    {
                        attachments.Photo.AlbumId = postResult.Response.Items[i].Attachments[j].Photo.AlbumId;
                        attachments.Photo.Date = postResult.Response.Items[i].Attachments[j].Photo.Date;
                        attachments.Photo.Id = postResult.Response.Items[i].Attachments[j].Photo.Id;
                        attachments.Photo.OwnerId = postResult.Response.Items[i].Attachments[j].Photo.OwnerId;
                        attachments.Photo.AccessKey = postResult.Response.Items[i].Attachments[j].Photo.AccessKey;
                        attachments.Photo.Text = postResult.Response.Items[i].Attachments[j].Photo.Text;
                        attachments.Photo.UserId = postResult.Response.Items[i].Attachments[j].Photo.UserId;

                        for (int k = 0; k < postResult.Response.Items[i].Attachments[j].Photo.Sizes.Count; k++)
                        {
                            var Size = new Size
                            {
                                Height = postResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Height,
                                Width = postResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Width,
                                Type = postResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Type,
                                Url = postResult.Response.Items[i].Attachments[j].Photo.Sizes[k].Url,
                            };

                            attachments.Photo.Sizes.Add(Size);
                        }
                    }

                    else if (attachments.Type == "link")
                    {
                        attachments.Link.Url = postResult.Response.Items[i].Attachments[j].Link.Url;
                        attachments.Link.Caption = postResult.Response.Items[i].Attachments[j].Link.Caption;
                        attachments.Link.Description = postResult.Response.Items[i].Attachments[j].Link.Description;
                        attachments.Link.Title = postResult.Response.Items[i].Attachments[j].Link.Title;
                    }
                    else if (attachments.Type == "doc")
                    {
                        attachments.Doc.Id = postResult.Response.Items[i].Attachments[j].Doc.Id;
                        attachments.Doc.OwnerId = postResult.Response.Items[i].Attachments[j].Doc.OwnerId;
                        attachments.Doc.Title = postResult.Response.Items[i].Attachments[j].Doc.Title;
                        attachments.Doc.Size = postResult.Response.Items[i].Attachments[j].Doc.Size;
                        attachments.Doc.Ext = postResult.Response.Items[i].Attachments[j].Doc.Ext;
                        attachments.Doc.Date = postResult.Response.Items[i].Attachments[j].Doc.Date;
                        attachments.Doc.Type = postResult.Response.Items[i].Attachments[j].Doc.Type;
                        attachments.Doc.Url = postResult.Response.Items[i].Attachments[j].Doc.Url;
                        attachments.Doc.IsUnsafe = postResult.Response.Items[i].Attachments[j].Doc.IsUnsafe;
                        attachments.Doc.AccessKey = postResult.Response.Items[i].Attachments[j].Doc.AccessKey;
                    }
                    else if (attachments.Type == "album")
                    {
                        attachments.Album.Created = postResult.Response.Items[i].Attachments[j].Album.Created;
                        attachments.Album.Id = postResult.Response.Items[i].Attachments[j].Album.Id;
                        attachments.Album.OwnerId = postResult.Response.Items[i].Attachments[j].Album.OwnerId;
                        attachments.Album.Size = postResult.Response.Items[i].Attachments[j].Album.Size;
                        attachments.Album.Title = postResult.Response.Items[i].Attachments[j].Album.Title;
                        attachments.Album.Updated = postResult.Response.Items[i].Attachments[j].Album.Updated;
                        attachments.Album.Description = postResult.Response.Items[i].Attachments[j].Album.Description;

                        attachments.Album.Thumb.AlbumId = postResult.Response.Items[i].Attachments[j].Album.Thumb.AlbumId;
                        attachments.Album.Thumb.Date = postResult.Response.Items[i].Attachments[j].Album.Thumb.Date;
                        attachments.Album.Thumb.Id = postResult.Response.Items[i].Attachments[j].Album.Thumb.Id;
                        attachments.Album.Thumb.OwnerId = postResult.Response.Items[i].Attachments[j].Album.Thumb.OwnerId;
                        attachments.Album.Thumb.AccessKey = postResult.Response.Items[i].Attachments[j].Album.Thumb.AccessKey;
                        attachments.Album.Thumb.Text = postResult.Response.Items[i].Attachments[j].Album.Thumb.Text;
                        attachments.Album.Thumb.UserId = postResult.Response.Items[i].Attachments[j].Album.Thumb.UserId;
                        attachments.Album.Thumb.WebViewToken = postResult.Response.Items[i].Attachments[j].Album.Thumb.WebViewToken;
                        attachments.Album.Thumb.HasTags = postResult.Response.Items[i].Attachments[j].Album.Thumb.HasTags;

                        for (int k = 0; k < postResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes.Count; k++)
                        {
                            var Size = new Size
                            {
                                Height = postResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Height,
                                Width = postResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Width,
                                Type = postResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Type,
                                Url = postResult.Response.Items[i].Attachments[j].Album.Thumb.Sizes[k].Url,
                            };

                            attachments.Album.Thumb.Sizes.Add(Size);
                        }
                    }
                    else if (attachments.Type == "video")
                    {
                        attachments.Video.Id = postResult.Response.Items[i].Attachments[j].Video.Id;
                        attachments.Video.OwnerId = postResult.Response.Items[i].Attachments[j].Video.OwnerId;
                        attachments.Video.Title = postResult.Response.Items[i].Attachments[j].Video.Title;
                        attachments.Video.Description = postResult.Response.Items[i].Attachments[j].Video.Description;
                        attachments.Video.Duration = postResult.Response.Items[i].Attachments[j].Video.Duration;
                        attachments.Video.Date = postResult.Response.Items[i].Attachments[j].Video.Date;
                        attachments.Video.AddingDate = postResult.Response.Items[i].Attachments[j].Video.AddingDate;
                        attachments.Video.Views = postResult.Response.Items[i].Attachments[j].Video.Views;
                        attachments.Video.ResponseType = postResult.Response.Items[i].Attachments[j].Video.ResponseType;
                        attachments.Video.AccessKey = postResult.Response.Items[i].Attachments[j].Video.AccessKey;
                        attachments.Video.CanLike = postResult.Response.Items[i].Attachments[j].Video.CanLike;
                        attachments.Video.CanRepost = postResult.Response.Items[i].Attachments[j].Video.CanRepost;
                        attachments.Video.CanSubscribe = postResult.Response.Items[i].Attachments[j].Video.CanSubscribe;
                        attachments.Video.CanAddToFaves = postResult.Response.Items[i].Attachments[j].Video.CanAddToFaves;
                        attachments.Video.CanAdd = postResult.Response.Items[i].Attachments[j].Video.CanAdd;
                        attachments.Video.Comments = postResult.Response.Items[i].Attachments[j].Video.Comments;
                        attachments.Video.TrackCode = postResult.Response.Items[i].Attachments[j].Video.TrackCode;
                        attachments.Video.Type = postResult.Response.Items[i].Attachments[j].Video.Type;
                        attachments.Video.Platform = postResult.Response.Items[i].Attachments[j].Video.Platform;
                        attachments.Video.CanDislike = postResult.Response.Items[i].Attachments[j].Video.CanDislike;

                        for (int k = 0; k < postResult.Response.Items[i].Attachments[j].Video.Images.Count; k++)
                        {
                            var Image = new Image
                            {
                                Height = postResult.Response.Items[i].Attachments[j].Video.Images[k].Height,
                                Width = postResult.Response.Items[i].Attachments[j].Video.Images[k].Width,
                                WithPadding = postResult.Response.Items[i].Attachments[j].Video.Images[k].WithPadding,
                                Url = postResult.Response.Items[i].Attachments[j].Video.Images[k].Url,
                            };

                            attachments.Video.Images.Add(Image);
                        }

                        for (int k = 0; k < postResult.Response.Items[i].Attachments[j].Video.FirstFrames.Count; k++)
                        {
                            var FirstFrame = new FirstFrame
                            {
                                Height = postResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Height,
                                Width = postResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Width,
                                Url = postResult.Response.Items[i].Attachments[j].Video.FirstFrames[k].Url,
                            };

                            attachments.Video.FirstFrames.Add(FirstFrame);
                        }
                    }

                    wallPost.PostAttachment.Add(attachments);
                }

                testResult.Posts.Add(wallPost);
                testResult.Count = postResult.Response.Count;
            }
        }
        else throw new VkApiException(postResult.WallPostError.ErrorCode, postResult.WallPostError.ErrorMessage);

        return testResult;
    }
}
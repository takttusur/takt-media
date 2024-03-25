﻿using RestSharp;
using TaktTusur.Media.Clients.VkApi.Internal.GroupInfoResponse;
using TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse;
using TaktTusur.Media.Clients.VkApi.Models;
using TaktTusur.Media.Clients.VkApi.Models.Enums;
using TaktTusur.Media.Clients.VkApi.Requests;

namespace TaktTusur.Media.Clients.VkApi;

public class VkApiClient : IVkApiClient
{
    private readonly VkApiOptions _options;
    private readonly RestClientOptions _restClientOptions;

    /// <summary>
    /// Создает экземпляр класса.
    /// </summary>
    /// <param name="options"></param>
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

    /// <summary>
    /// Запрашивает информацию о сообществе.
    /// </summary>
    /// <param name="groupId">Идентификатор сообщества.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="VkApiException"></exception>
    public async Task<VkGroupInfo> GetGroupInfoAsync(string groupId, CancellationToken cancellationToken)
    {
        var groupRequest = new GroupByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key,
            GroupId = groupId,
            Fields = new string[] { "start_date", "finish_date", "description" }
        };

        var info = new VkGroupInfo();
        var client = new RestClient(_restClientOptions);

        var result = await client.GetJsonAsync<GroupByIdResponse>("groups.getById", groupRequest, cancellationToken);

        if (result.GroupInfoError == null)
        {
            info.Id = result.Response.Groups[0].Id;
            info.Name = result.Response.Groups[0].Name;
            info.ScreenName = result.Response.Groups[0].ScreenName;
            info.Type = GetGroupTypeFromString(result.Response.Groups[0].Type);
            info.Photo50 = result.Response.Groups[0].Photo50;
            info.Photo100 = result.Response.Groups[0].Photo100;
            info.Photo200 = result.Response.Groups[0].Photo200;
            info.AccessType = GetAccessTypeFromInt(result.Response.Groups[0].IsClosed);
            info.URL = $"https://vk.com/public{info.Id}";

            if (info.Type != VkGroupTypes.Event) return info;

            info.StartDateTime = result.Response.Groups[0].StartDate != 0 
                ? DateTimeOffset.FromUnixTimeSeconds(result.Response.Groups[0].StartDate) 
                : null;
            info.FinishDateTime = result.Response.Groups[0].FinishDate != 0 
                ? DateTimeOffset.FromUnixTimeSeconds(result.Response.Groups[0].FinishDate) 
                : null;
            info.Description = result.Response.Groups[0].Description;
        }
        else throw new VkApiException(result.GroupInfoError.ErrorCode, result.GroupInfoError.ErrorMessage);

        return info;
    }

    /// <summary>
    /// Запрашивает записи на стене сообщества.
    /// </summary>
    /// <param name="groupId">Идентификатор сообщества.</param>
    /// <param name="count">Количество записей, которое необходимо получить. Максимальное значение: 100</param>
    /// <param name="cancellationToken">Ключ доступа.</param>
    /// <returns></returns>
    /// <exception cref="VkApiException"></exception>
    public async Task<VkPosts> GetPostsAsync(string groupId, int count, CancellationToken cancellationToken)
    {
        var wallRequest = new WallByIdRequest()
        {
            Version = "5.199",
            AccessToken = _options.Key,
            Domain = groupId,
            Count = count,
            Extended = 1
        };

        var testResult = new VkPosts();
        var client = new RestClient(_restClientOptions);
        var postResult = await client.GetJsonAsync<WallPostByIdResponse>("wall.get", wallRequest, cancellationToken);

        if (postResult?.WallPostError != null)
            throw new VkApiException(postResult.WallPostError.ErrorCode, postResult.WallPostError.ErrorMessage);

        if (postResult?.Response?.Items == null)
            return new VkPosts()
            {
                Posts = new List<WallPost>(),
                Count = 0
            };

        foreach (var t in postResult?.Response?.Items)
        {
            var wallPost = MapPostDtoToWallPost(t);

            testResult.Posts.Add(wallPost);
            testResult.Count = postResult.Response.Count;
        }

        return testResult;
    }


    private WallPost MapPostDtoToWallPost(PostDto t)
    {
        var wallPost = new WallPost
        {
            Id = t.Id,
            SourceId = Math.Abs(t.OwnerId),
            PostText = t.Text,
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds(t.Date),
            PostType = GetPostTypeFromString(t.Type),
        };
        wallPost.PostURL = $"https://vk.com/wall-{wallPost.SourceId}_{wallPost.Id}";

        wallPost.PostCopyrightNotes.Id = t.Copyrights.Id;
        wallPost.PostCopyrightNotes.Link = t.Copyrights.Link;
        wallPost.PostCopyrightNotes.Name = t.Copyrights.Name;
        wallPost.PostCopyrightNotes.Type = t.Copyrights.Type;

        foreach (var attachment in t.Attachments)
        {
            var attachments = new Models.Common.Attachment
            {
                Type = attachment.Type,
            };

            switch (attachments.Type)
            {
                case "photo":
                    {
                        attachments.Photo.AlbumId = attachment.Photo.AlbumId;
                        attachments.Photo.Date = attachment.Photo.Date;
                        attachments.Photo.Id = attachment.Photo.Id;
                        attachments.Photo.OwnerId = attachment.Photo.OwnerId;
                        attachments.Photo.AccessKey = attachment.Photo.AccessKey;
                        attachments.Photo.Text = attachment.Photo.Text;
                        attachments.Photo.UserId = attachment.Photo.UserId;

                        for (int k = 0; k < attachment.Photo.Sizes.Count; k++)
                        {
                            var size = new Models.Common.Size
                            {
                                Height = attachment.Photo.Sizes[k].Height,
                                Width = attachment.Photo.Sizes[k].Width,
                                Type = attachment.Photo.Sizes[k].Type,
                                Url = attachment.Photo.Sizes[k].Url,
                            };

                            attachments.Photo.Sizes.Add(size);
                        }

                        break;
                    }
                case "link":
                    attachments.Link.Url = attachment.Link.Url;
                    attachments.Link.Caption = attachment.Link.Caption;
                    attachments.Link.Description = attachment.Link.Description;
                    attachments.Link.Title = attachment.Link.Title;
                    break;
                case "doc":
                    attachments.Doc.Id = attachment.Doc.Id;
                    attachments.Doc.OwnerId = attachment.Doc.OwnerId;
                    attachments.Doc.Title = attachment.Doc.Title;
                    attachments.Doc.Size = attachment.Doc.Size;
                    attachments.Doc.Ext = attachment.Doc.Ext;
                    attachments.Doc.Date = attachment.Doc.Date;
                    attachments.Doc.Type = attachment.Doc.Type;
                    attachments.Doc.Url = attachment.Doc.Url;
                    attachments.Doc.IsUnsafe = attachment.Doc.IsUnsafe;
                    attachments.Doc.AccessKey = attachment.Doc.AccessKey;
                    break;
                case "album":
                    {
                        attachments.Album.Created = attachment.Album.Created;
                        attachments.Album.Id = attachment.Album.Id;
                        attachments.Album.OwnerId = attachment.Album.OwnerId;
                        attachments.Album.Size = attachment.Album.Size;
                        attachments.Album.Title = attachment.Album.Title;
                        attachments.Album.Updated = attachment.Album.Updated;
                        attachments.Album.Description = attachment.Album.Description;

                        attachments.Album.Thumb.AlbumId = attachment.Album.Thumb.AlbumId;
                        attachments.Album.Thumb.Date = attachment.Album.Thumb.Date;
                        attachments.Album.Thumb.Id = attachment.Album.Thumb.Id;
                        attachments.Album.Thumb.OwnerId = attachment.Album.Thumb.OwnerId;
                        attachments.Album.Thumb.AccessKey = attachment.Album.Thumb.AccessKey;
                        attachments.Album.Thumb.Text = attachment.Album.Thumb.Text;
                        attachments.Album.Thumb.UserId = attachment.Album.Thumb.UserId;
                        attachments.Album.Thumb.WebViewToken = attachment.Album.Thumb.WebViewToken;
                        attachments.Album.Thumb.HasTags = attachment.Album.Thumb.HasTags;

                        for (int k = 0; k < attachment.Album.Thumb.Sizes.Count; k++)
                        {
                            var size = new Models.Common.Size
                            {
                                Height = attachment.Album.Thumb.Sizes[k].Height,
                                Width = attachment.Album.Thumb.Sizes[k].Width,
                                Type = attachment.Album.Thumb.Sizes[k].Type,
                                Url = attachment.Album.Thumb.Sizes[k].Url,
                            };

                            attachments.Album.Thumb.Sizes.Add(size);
                        }

                        break;
                    }
                case "video":
                    {
                        attachments.Video.Id = attachment.Video.Id;
                        attachments.Video.OwnerId = attachment.Video.OwnerId;
                        attachments.Video.Title = attachment.Video.Title;
                        attachments.Video.Description = attachment.Video.Description;
                        attachments.Video.Duration = attachment.Video.Duration;
                        attachments.Video.Date = attachment.Video.Date;
                        attachments.Video.AddingDate = attachment.Video.AddingDate;
                        attachments.Video.Views = attachment.Video.Views;
                        attachments.Video.ResponseType = attachment.Video.ResponseType;
                        attachments.Video.AccessKey = attachment.Video.AccessKey;
                        attachments.Video.CanLike = attachment.Video.CanLike;
                        attachments.Video.CanRepost = attachment.Video.CanRepost;
                        attachments.Video.CanSubscribe = attachment.Video.CanSubscribe;
                        attachments.Video.CanAddToFaves = attachment.Video.CanAddToFaves;
                        attachments.Video.CanAdd = attachment.Video.CanAdd;
                        attachments.Video.Comments = attachment.Video.Comments;
                        attachments.Video.TrackCode = attachment.Video.TrackCode;
                        attachments.Video.Type = attachment.Video.Type;
                        attachments.Video.Platform = attachment.Video.Platform;
                        attachments.Video.CanDislike = attachment.Video.CanDislike;

                        for (int k = 0; k < attachment.Video.Images.Count; k++)
                        {
                            var image = new Models.Common.Image
                            {
                                Height = attachment.Video.Images[k].Height,
                                Width = attachment.Video.Images[k].Width,
                                WithPadding = attachment.Video.Images[k].WithPadding,
                                Url = attachment.Video.Images[k].Url,
                            };

                            attachments.Video.Images.Add(image);
                        }

                        for (int k = 0; k < attachment.Video.FirstFrames.Count; k++)
                        {
                            var firstFrame = new Models.Common.FirstFrame
                            {
                                Height = attachment.Video.FirstFrames[k].Height,
                                Width = attachment.Video.FirstFrames[k].Width,
                                Url = attachment.Video.FirstFrames[k].Url,
                            };

                            attachments.Video.FirstFrames.Add(firstFrame);
                        }

                        break;
                    }
                case "event":
                    attachments.Event.Id = attachment.Event.Id;
                    attachments.Event.EventStartDateTime = attachment.Event.EventStartDateTime;
                    attachments.Event.MemberStatus = attachment.Event.MemberStatus;
                    attachments.Event.IsFavorite = attachment.Event.IsFavorite;
                    attachments.Event.Address = attachment.Event.Address;
                    attachments.Event.Text = attachment.Event.Text;
                    attachments.Event.ButtonText = attachment.Event.ButtonText;
                    break;
            }

            wallPost.PostAttachment.Add(attachments);
        }

        foreach (var copyPost in t.CopyHistory.Select(copy => new WallPost()
        {
            Id = copy.Id,
            SourceId = Math.Abs(copy.OwnerId),
            PostText = copy.Text,
            CreatedAt = DateTimeOffset.FromUnixTimeSeconds(copy.Date),
            PostType = GetPostTypeFromString(copy.Type),
        }))
        {
            wallPost.InnerPosts.Add(copyPost);
        }

        return wallPost;
    }
    
    private WallPostTypes GetPostTypeFromString(string postType)
    {
        return postType switch
        {
            "post" => WallPostTypes.Post,
            "copy" => WallPostTypes.Copy,
            "reply" => WallPostTypes.Reply,
            "postpone" => WallPostTypes.Postpone,
            "suggest" => WallPostTypes.Suggest,
            _ => WallPostTypes.Unknown
        };
    }

    private VkGroupTypes GetGroupTypeFromString(string groupType)
    {
        return groupType switch
        {
            "group" => VkGroupTypes.Group,
            "page" => VkGroupTypes.Page,
            "event" => VkGroupTypes.Event,
            _ => VkGroupTypes.Unknown
        };
    }

    private AccessTypes GetAccessTypeFromInt(int accessType)
    {
        return accessType switch
        {
            0 => AccessTypes.Accessible,
            1 => AccessTypes.Inaccessible,
            2 => AccessTypes.Private,
            _ => AccessTypes.Unknown
        };
    }
}
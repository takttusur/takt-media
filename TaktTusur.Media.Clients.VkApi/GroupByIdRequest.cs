﻿using RestSharp;

namespace TaktTusur.Media.Clients.VkApi;

public class GroupByIdRequest
{
    [RequestProperty (Name = "group_id")]
    public string GroupId { get; set; }

    [RequestProperty(Name = "access_token")]
    public string AccessToken { get; set; }

    [RequestProperty(Name = "v")]
    public string Version { get; set; }
}
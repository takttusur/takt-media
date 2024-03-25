﻿using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class FirstFrame
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
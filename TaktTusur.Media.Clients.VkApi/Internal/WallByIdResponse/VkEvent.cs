﻿using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class VkEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("time")]
        public long EventStartDateTime { get; set; }

        [JsonPropertyName("member_status")]
        public int MemberStatus { get; set; }

        [JsonPropertyName("is_favorite")]
        public bool IsFavorite { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("button_text")]
        public string ButtonText { get; set; }

        [JsonPropertyName("friends")]
        public int[] Friends { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.WallByIdResponse
{
    internal class Video
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("image")]
        public List<Image> Images { get; set; } = new List<Image>();

        [JsonPropertyName("first_frame")]
        public List<FirstFrame> FirstFrames { get; set; } = new List<FirstFrame>();

        [JsonPropertyName("date")]
        public int Date { get; set; }

        [JsonPropertyName("adding_date")]
        public int AddingDate { get; set; }

        [JsonPropertyName("views")]
        public int Views { get; set; }

        [JsonPropertyName("response_type")]
        public string ResponseType { get; set; }

        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }

        [JsonPropertyName("can_like")]
        public int CanLike { get; set; }

        [JsonPropertyName("can_repost")]
        public int CanRepost { get; set; }

        [JsonPropertyName("can_subscribe")]
        public int CanSubscribe { get; set; }

        [JsonPropertyName("can_add_to_faves")]
        public int CanAddToFaves { get; set; }

        [JsonPropertyName("can_add")]
        public int CanAdd { get; set; }

        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("track_code")]
        public string TrackCode { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        [JsonPropertyName("can_dislike")]
        public int CanDislike { get; set; }
    }
}
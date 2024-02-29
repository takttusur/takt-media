using System.Text.Json.Serialization;

namespace TaktTusur.Media.Clients.VkApi.Internal.GroupInfoResponse
{
    internal class Group
    {
        #region Base

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("screen_name")]
        public string ScreenName { get; set; }

        [JsonPropertyName("Is_closed")]
        public int IsClosed { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("photo_50")]
        public string Photo50 { get; set; }

        [JsonPropertyName("photo_100")]
        public string Photo100 { get; set; }

        [JsonPropertyName("photo_200")]
        public string Photo200 { get; set; }

        #endregion

        #region Optional

        /// <summary>
        /// Для встреч содержат время начала встречи в формате unixtime.
        /// Для публичных страниц содержит только start_date — дата основания в формате YYYYMMDD.
        /// </summary>
        [JsonPropertyName("start_date")]
        public long StartDate { get; set; }

        /// <summary>
        /// Для встреч содержат время окончания встречи в формате unixtime.
        /// </summary>
        [JsonPropertyName("finish_date")]
        public long FinishDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        #endregion
    }
}
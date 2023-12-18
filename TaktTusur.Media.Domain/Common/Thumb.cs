namespace TaktTusur.Media.Domain.Common
{
    public class Thumb
    {
        public int AlbumId { get; set; }

        public int Date { get; set; }

        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string AccessKey { get; set; }

        public List<Size> Sizes { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public string WebViewToken { get; set; }

        public bool HasTags { get; set; }
    }
}
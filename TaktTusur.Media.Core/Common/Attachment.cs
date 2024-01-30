namespace TaktTusur.Media.Core.Common
{
    public class Attachment
    {
        public string Type { get; set; }

        public Doc Doc { get; set; } = new Doc();

        public Link Link { get; set; } = new Link();

        public Photo Photo { get; set; } = new Photo();

        public Album Album { get; set; } = new Album();

        public Video Video { get; set; } = new Video();
    }
}
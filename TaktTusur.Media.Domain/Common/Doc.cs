namespace TaktTusur.Media.Domain.Common
{
    public class Doc
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string Title { get; set; }

        public int Size { get; set; }

        public string Ext { get; set; }

        public int Date { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public int IsUnsafe { get; set; }

        public string AccessKey { get; set; }
    }
}
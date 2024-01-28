namespace TaktTusur.Media.Domain.Common
{
    public class Album
    {
        public int Created { get; set; }

        public int Id { get; set; }

        public int OwnerId { get; set; }

        public int Size { get; set; }

        public string Title { get; set; }

        public int Updated { get; set; }

        public string Description { get; set; }

        public Thumb Thumb { get; set; } = new Thumb();
    }
}
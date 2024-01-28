using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaktTusur.Media.Domain.Common
{
    public class Video
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Duration { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public List<FirstFrame> FirstFrames { get; set; } = new List<FirstFrame>();

        public int Date { get; set; }

        public int AddingDate { get; set; }

        public int Views { get; set; }

        public string ResponseType { get; set; }

        public string AccessKey { get; set; }

        public int CanLike { get; set; }

        public int CanRepost { get; set; }

        public int CanSubscribe { get; set; }

        public int CanAddToFaves { get; set; }

        public int CanAdd { get; set; }

        public int Comments { get; set; }

        public string TrackCode { get; set; }

        public string Type { get; set; }

        public string Platform { get; set; }

        public int CanDislike { get; set; }
    }
}
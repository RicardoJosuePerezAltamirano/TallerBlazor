using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorTallerLive.Shared
{
    public class Photo
    {
        public int albumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
namespace Entities
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
    }
}

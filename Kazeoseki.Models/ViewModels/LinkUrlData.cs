using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Models.ViewModels
{
    public class LinkUrlData
    {
        public string Username { get; set; }
        public string UserIconUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}

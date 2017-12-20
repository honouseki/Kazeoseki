using HtmlAgilityPack;
using Kazeoseki.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Services.Services
{
    public class LinkUrlDataService : BaseService
    {
        public LinkUrlData Get()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //temporarily hardcoded
            string url = "https://kazeoseki.deviantart.com";

            LinkUrlData model = new LinkUrlData();
            model.Url = url;
            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            //var items1 = document.DocumentNode.Descendants("div")
            //    .Where(node => node.GetAttributeValue("class", "")
            //    .Equals("authorative-avatar")).ToList();

            string items2 = document.DocumentNode.SelectNodes("//img")
                // No class 'ghost-edit' in here...this works... if anything
                //.Where(d => d.Attributes.Contains("class")
                //    &&
                //    d.Attributes["class"].Value.Contains("ghost-edit"))
                .First()
                .Attributes["src"].Value;
            
            //string item2 = document.DocumentNode
            //        .SelectNodes("//a/[@class='ghost-edit']")
            //        .First()
            //        .Attributes["href"].Value;
            model.UserIconUrl = items2;
            return model;
        }
    }
}


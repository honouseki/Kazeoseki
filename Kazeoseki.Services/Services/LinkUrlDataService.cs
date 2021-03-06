﻿using HtmlAgilityPack;
using Kazeoseki.Models.Domain;
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
        public LinkUrlData Get(Url url)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //temporarily hardcoded ; this will be a parameter
            //string url = "https://kazeoseki.deviantart.com";

            LinkUrlData model = new LinkUrlData();
            model.Url = url.UrlString;
            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url.UrlString);

            // Grabs info for UserIconUrl
            var anchortag1 = document.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("authorative-avatar")).ToList();
            model.UserIconUrl = anchortag1[0].Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

            // Grabs Username / TagLine
            var anchortag2 = document.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("gruserbadge")).ToList();
            model.Username = anchortag2[0].Descendants("a").FirstOrDefault().InnerText;
            model.TagLine = anchortag2[0].LastChild.InnerText;
            
            return model;
        }
    }
}


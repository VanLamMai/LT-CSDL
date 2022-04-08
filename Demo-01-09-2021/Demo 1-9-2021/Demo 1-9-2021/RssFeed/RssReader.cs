using System;
using System.Collections.Generic;
using System.Text;
using Demo_1_9_2021.Models;
using System.Net;


namespace Demo_1_9_2021.RssFeed
{
    public class RssReader
    {
        private readonly NewsParser _parser;
        public RssReader(NewsParser parser)
        {
            _parser = parser;
        }
        public List<Article> GetNews(string rssLink)
        {
            var feedData = DownloadFeed(rssLink);
            return _parser.ParseXml(feedData);
        }

        private string DownloadFeed(string rssLink)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
            return client.DownloadString(rssLink);
        }
    }
}

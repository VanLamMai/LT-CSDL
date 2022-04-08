using System;
using System.Collections.Generic;
using System.Text;
using Demo_1_9_2021.IO;
using Demo_1_9_2021.Models;
using System.Threading.Tasks;
using Demo_1_9_2021.RssFeed;

namespace Demo_1_9_2021
{
    public class NewFeedManager
    {
        private readonly INewsRepository _newsRepository;
        private List<Publisher> _publishers;
        private readonly RssReader _rssReader;

        public NewFeedManager(INewsRepository newsRepository, RssReader rssReader)
        {
            _newsRepository = newsRepository;
            _rssReader = rssReader;
        }
        public List<Publisher> GetNewsFeed()
        {
            if (_publishers == null)
            {
                _publishers = _newsRepository.GetNews();
                
            }
            return _publishers;
        }
        public void SaveChanger()
        {
            _newsRepository.Save(_publishers);
        }
        public void RemovePublisher(string publisherName)
        {
            _publishers.RemoveAll(x => x.Name == publisherName);
            SaveChanger();

        }
        public void RemoveCategory(string publisherName, String categoryName)
        {
            var publisher = _publishers.Find(x => x.Name == publisherName);
            if (publisher == null) return;

            publisher.RemovCategory(categoryName);
            SaveChanger();
        }
        public bool AddCategory(string publisherName,string categoryName, string rssLink, bool updateIfExisted)
        {
            var publisher = _publishers.Find(x => x.Name == publisherName);
            if (publisher == null)
            {
                publisher = new Publisher()
                {
                    Name = publisherName
                };
                _publishers.Add(publisher);
            }
            return publisher.AddCategory(categoryName, rssLink, updateIfExisted);
        }
        public List<Article> GetNews(string publisherName, string categoryName)
        {
            var publisher = _publishers.Find(x => x.Name == publisherName);
            if (publisher == null) return new List<Article>();

            var category = publisher.Categories.Find(x => x.Name == categoryName);
            if (category == null) return new List<Article>();

            if (category.Articles.Count == 0)
            {
                category.Articles = _rssReader.GetNews(category.RssLink);
            }

            return category.Articles;
        }




    }
}

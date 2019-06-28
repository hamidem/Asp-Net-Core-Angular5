using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private Feed RssFeed = new Feed();

        /// <summary>
        /// GetRandArticles : gets a list of random feeds for the Home Page
        /// </summary>
        /// <returns>IEnumerable Feed consumed by Angular service</returns>
        [HttpGet("[action]/")]
        public IEnumerable<Feed> GetRandArticles()
        {
            RssFeed.FeedList = new List<Feed>();
            RssFeed.GetRandFeedsAsync();

            if (RssFeed.FeedList == null || RssFeed.FeedList.Count < 1)
                return null;

            var feedList = Enumerable.Range(0, RssFeed.FeedList.Count).Select(index => new Feed
            {
                Media_name = RssFeed.FeedList.ElementAt(index).Media_name,
                Title = RssFeed.FeedList.ElementAt(index).Title,
                Description = RssFeed.FeedList.ElementAt(index).Description,
                Image_url = RssFeed.FeedList.ElementAt(index).Image_url,
                Published = RssFeed.FeedList.ElementAt(index).Published,
                Link = RssFeed.FeedList.ElementAt(index).Link
            }).ToList();

            return feedList == null || feedList.Count < 1 ? null : feedList;
        }

        /// <summary>
        /// GetArticles : gets a list of feeds corresponding to a category and a url
        /// </summary>
        /// <param name="cat"> int category</param>
        /// <param name="url">int url</param>
        /// <returns>IEnumerable Feed consumed by Angular service</returns>
        [HttpGet("[action]/{cat}/{url}")]
        public IEnumerable<Feed> GetArticles(int cat, int url)
        {
            RssFeed.FeedList = new List<Feed>();
            RssFeed.GetFeedsAsync(cat, url);

            if (RssFeed.FeedList == null || RssFeed.FeedList.Count <= 0)
                return null;

            return Enumerable.Range(0, RssFeed.FeedList.Count).Select(index => new Feed
            {
                Media_name = RssFeed.FeedList.ElementAt(index).Media_name,
                Title = RssFeed.FeedList.ElementAt(index).Title,
                Description = RssFeed.FeedList.ElementAt(index).Description,
                Image_url = RssFeed.FeedList.ElementAt(index).Image_url,
                Published = RssFeed.FeedList.ElementAt(index).Published,
                Link = RssFeed.FeedList.ElementAt(index).Link
            }).ToList();
        }
    }
}

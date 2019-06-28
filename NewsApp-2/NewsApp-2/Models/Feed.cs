using System;
using System.Collections.Generic;
using System.Xml;
using NewsApp.Tools;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsApp.Models
{
    /// <summary>
    /// The Feed Class : To create a Feed object and get a list of feeds
    /// </summary>
    public class Feed
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image_url { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Published { get; set; }
        public string Link { get; set; }
        public string Media_name { get; set; }
        public List<Feed> FeedList { get; set; }

        private List<Feed> TmpFeeds = new List<Feed>();
        private List<string> UrlList = new List<string>();
        private int FeedIndex = -1;
        private Feed RssFeed { get; set; }

        /// <summary>
        /// GetFeedsAsync : Gets feeds by category and url
        /// </summary>
        /// <remarks>
        /// Iterates in the UrlList returned by SetUrlList(cat, url) 
        /// and fills the FeedList witch is used by the controller
        /// </remarks>
        /// <param name="cat">category of feed</param>
        /// <param name="url">url of feed</param>
        public async void GetFeedsAsync(int cat, int url)
        {
            UrlList = FeedUrlHandler.SetUrlList(cat, url);
            FeedList = new List<Feed>();

            foreach (var feedUrl in UrlList)
            {
                XmlReader reader = XmlReader.Create(feedUrl);
                using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings() { Async = true }))
                {
                    var feedReader = new RssFeedReader(xmlReader);
                    while (await feedReader.Read())
                    {
                        try
                        {
                            switch (feedReader.ElementType)
                            {
                                //// Read category
                                //case SyndicationElementType.Category:
                                //    ISyndicationCategory category = await feedReader.ReadCategory();
                                //    break;

                                //// Read Image
                                //case SyndicationElementType.Image:
                                //    ISyndicationImage image = await feedReader.ReadImage();
                                //    break;

                                // Read Item
                                case SyndicationElementType.Item:
                                    ISyndicationItem item = await feedReader.ReadItem();
                                    if (!string.IsNullOrEmpty(item?.Title)
                                    && !string.IsNullOrEmpty(item?.Description)
                                    && !string.IsNullOrEmpty(item?.Links?.FirstOrDefault(
                                                    l => l.MediaType == "image/jpeg")
                                                    ?.Uri?.ToString()))
                                    {
                                        string title = Regex.Replace(item?.Title, "<.*?>", String.Empty);
                                        string description = Regex.Replace(item?.Description, "<.*?>", String.Empty);

                                        Feed feed = new Feed
                                        {
                                            Media_name = FeedUrlHandler.MediaNameList.ElementAt(cat - 1).ElementAt(url),
                                            Title = title,
                                            Description = description,
                                            Link = item?.Links?.FirstOrDefault(l => l.RelationshipType == "alternate")?.Uri?.ToString(),
                                            Published = item?.Published.DateTime.ToShortDateString(),
                                            Image_url = item?.Links?.FirstOrDefault(l => l.MediaType == "image/jpeg")?.Uri?.ToString()
                                        };

                                        FeedList.Add(feed);
                                    }
                                    break;
                                    //// Read link
                                    //case SyndicationElementType.Link:
                                    //    ISyndicationLink link = await feedReader.ReadLink();
                                    //    break;

                                    //// Read Person
                                    //case SyndicationElementType.Person:
                                    //    ISyndicationPerson person = await feedReader.ReadPerson();
                                    //    break;

                                    //// Read content
                                    //default:
                                    //    ISyndicationContent content = await feedReader.ReadContent();
                                    //    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            throw ex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// GetRandFeedsAsync : Gets Rand feeds
        /// </summary>
        /// <remarks>
        /// Iterates through the UrlList containing all urls 
        /// and fills the TmpFeeds
        /// Iterates through the TmpFeeds to fill the FeedList
        /// with random feeds
        /// </remarks>
        /// <param name="cat">category of feed</param>
        /// <param name="url">url of feed</param>
        public async void GetRandFeedsAsync()
        {
            //prevents from filling the TmpFeeds if it's allready filled
            if (TmpFeeds?.Count < 1)
            {
                UrlList = FeedUrlHandler.UrlList;

                foreach (var url in UrlList)
                {
                    FeedIndex++;

                    XmlReader reader = XmlReader.Create(url);
                    var xmlReader = XmlReader.Create(reader, new XmlReaderSettings() { Async = true });
                    var feedReader = new RssFeedReader(xmlReader);
                    while (await feedReader.Read())
                    {
                        try
                        {
                            switch (feedReader.ElementType)
                            {
                                case SyndicationElementType.Item:
                                    ISyndicationItem item = await feedReader.ReadItem();
                                    if (!string.IsNullOrEmpty(item?.Title)
                                            && !string.IsNullOrEmpty(item?.Description)
                                            && !string.IsNullOrEmpty(item?.Links?.FirstOrDefault(
                                                            l => l.MediaType == "image/jpeg")
                                                            ?.Uri?.ToString()))
                                    {
                                        TmpFeeds.Add(new Feed
                                        {
                                            Media_name = FeedUrlHandler.AllMediaName.ElementAt(FeedIndex),
                                            Title = item?.Title,
                                            Description = item?.Description,
                                            Link = item?.Links?.FirstOrDefault(l => l.RelationshipType == "alternate")?.Uri?.ToString(),
                                            Published = item?.Published.DateTime.ToShortDateString(),
                                            Image_url = item?.Links?.FirstOrDefault(l => l.MediaType == "image/jpeg")?.Uri?.ToString()
                                        });
                                    }
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            throw ex;
                        }
                    }
                }
            }
            if (TmpFeeds?.Count > 0)
            {
                FeedList = new List<Feed>();
                foreach (var feed in TmpFeeds)
                {
                    Random ranId = new Random();
                    var rand = ranId.Next(0, TmpFeeds.Count);

                    string title = Regex.Replace(TmpFeeds.ElementAt(rand).Title, "<.*?>", String.Empty);
                    string description = Regex.Replace(TmpFeeds.ElementAt(rand).Description, "<.*?>", String.Empty);

                    RssFeed = new Feed
                    {
                        Media_name = TmpFeeds.ElementAt(rand).Media_name,
                        Title = title,
                        Description = description,
                        Link = TmpFeeds.ElementAt(rand).Link,
                        Published = TmpFeeds.ElementAt(rand).Published,
                        Image_url = TmpFeeds.ElementAt(rand).Image_url
                    };

                    FeedList.Add(RssFeed);
                }
            }
            FeedIndex = -1;
        }
    }
}

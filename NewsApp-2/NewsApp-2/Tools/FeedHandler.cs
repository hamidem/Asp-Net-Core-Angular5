
using System.Collections.Generic;
using System.Linq;

namespace NewsApp.Tools
{
    /// <summary>
    /// The FeedHandler Class
    /// </summary>
    public static class FeedUrlHandler
    {
        public static string[] AllMediaName = { "Le Point", "L'Express", "La Croix",
                                                "Le Monde", "L'Express", "Courrier-International",
                                                "Le Monde", "Le Point", "Courrier-International",
                                                "Le Point", "L'Equipe", "Sport.fr" };

        public static string[] ActuMediaName = { "Le Point", "L'Express", "La Croix",
                                                 "Courrier-International"};

        public static string[] ScienceMediaName = { "L'Express", "Courrier-International" };

        public static string[] CultureMediaName = { "Le Point", "Courrier-International" };

        public static string[] SportMediaName = { "Le Point", "L'Equipe", "Sport.fr" };

        public static List<string[]> MediaNameList = new List<string[]>
        {
            ActuMediaName, ScienceMediaName, CultureMediaName, SportMediaName
        };

        public static List<string> UrlList = new List<string>
        {
            "http://www.lepoint.fr/24h-infos/rss.xml",
            "http://www.lexpress.fr/rss/alaune.xml",
            "http://www.la-croix.com/RSS/UNIVERS",
            "http://www.lemonde.fr/sciences/rss_full.xml",
            "http://www.lexpress.fr/rss/science-et-sante.xml",
            "http://www.courrierinternational.com/feed/category/6268/rss.xml",
            "http://www.lemonde.fr/culture/rss_full.xml",
            "http://www.lepoint.fr/culture/rss.xml",
            "http://www.courrierinternational.com/feed/category/6270/rss.xml",
            "http://www.lepoint.fr/sport/rss.xml",
            "http://www.lequipe.fr/rss/actu_rss.xml",
            "http://www.sports.fr/fr/cmc/rss.xml"
        };
        
        public static List<string> ActuUrlList = new List<string>
        {"http://www.lepoint.fr/24h-infos/rss.xml",
            "http://www.lexpress.fr/rss/alaune.xml",
            "http://www.la-croix.com/RSS/UNIVERS",
            "http://www.courrierinternational.com/feed/category/6260/rss.xml"
        };

        public static List<string> ScienceUrlList = new List<string>
        {
           "http://www.lexpress.fr/rss/science-et-sante.xml",
           "http://www.courrierinternational.com/feed/category/6268/rss.xml"
        };

        public static List<string> CultureUrlList = new List<string>
        {
            "http://www.lepoint.fr/culture/rss.xml",
            "http://www.courrierinternational.com/feed/category/6270/rss.xml"
        };

        public static List<string> SportUrlList = new List<string>
        {
            "http://www.lepoint.fr/sport/rss.xml",
            "http://www.lequipe.fr/rss/actu_rss.xml",
            "http://www.sports.fr/fr/cmc/rss.xml"
        };

        /// <summary>
        /// SetUrlList : Sets a url list from constants and returns it
        /// </summary>
        /// <param name="catIndex">int - The category Index</param>
        /// <param name="urlIndex">int - The url Index</param>
        /// <returns>string - A list of urls</returns>
        public static List<string> SetUrlList(int catIndex, int urlIndex)
        {
            List<string> urlList = new List<string>();
            switch (catIndex)
            {
                case 2 when (urlIndex < 0):
                    urlList.AddRange(ScienceUrlList);
                    break;
                case 2:
                    urlList.Add(ScienceUrlList.ElementAt(urlIndex));
                    break;
                case 3 when (urlIndex >= 0):
                    urlList.Add(CultureUrlList.ElementAt(urlIndex));
                    break;
                case 3:
                    urlList.AddRange(CultureUrlList);
                    break;
                case 4 when (urlIndex >= 0):
                    urlList.Add(SportUrlList.ElementAt(urlIndex));
                    break;
                case 4:
                    urlList.AddRange(SportUrlList);
                    break;
                case 5:
                    urlList.AddRange(UrlList);
                    break;
                default:
                    if (urlIndex >= 0)
                        urlList.Add(ActuUrlList.ElementAt(urlIndex));
                    else
                        urlList.AddRange(ActuUrlList);
                    break;
            }

            return urlList;
        }
    }
}

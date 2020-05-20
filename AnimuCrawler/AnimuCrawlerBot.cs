using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AnimuCrawler
{
    class AnimuCrawlerBot
    {
        private static readonly Regex UrlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);
        private static readonly Regex HrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        private static readonly Regex episodePattern = new Regex("(episode|ep)-(\\d+)", RegexOptions.IgnoreCase);

        private static readonly WebClient Client = new WebClient();

        private readonly Uri watchLink;

        public AnimuCrawlerBot(string link) {
            watchLink = new UriBuilder(link).Uri;


        }

        public void Watch() {

            string webPage = Client.DownloadString(watchLink);
            MatchCollection links = UrlTagPattern.Matches(webPage);
            foreach (Match href in links)
            {
                string newUrl = HrefPattern.Match(href.Value).Groups[1].Value;
                Uri absoluteUrl = null;
                if (episodePattern.IsMatch(newUrl))
                {
                    Match episode = episodePattern.Match(newUrl);
                    Console.WriteLine(episode.Value);
                    absoluteUrl = NormalizeUrl(watchLink, newUrl);
                }
                if (absoluteUrl != null  && (absoluteUrl.Scheme == Uri.UriSchemeHttp || absoluteUrl.Scheme == Uri.UriSchemeHttps))
                {
                    
                }
            }
        }

        private static Uri NormalizeUrl(Uri hostUrl, string url)
        {
            bool urlOk = Uri.TryCreate(hostUrl, url, out var absoluteUrl);
            if (urlOk)
            {
                return absoluteUrl.ToString().EndsWith("/") ?
                    absoluteUrl : new UriBuilder(absoluteUrl + "/").Uri;
            }
            else
            {
                return null;
            }
        }
    }
}

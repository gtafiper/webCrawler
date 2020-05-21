using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AnimuCrawler
{
    class AnimuCrawlerBot
    {
        private static readonly Regex UrlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);
        private static readonly Regex HrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        private static readonly Regex episodePattern = new Regex("\\/([A-Za-z0-9_-]+)(?:episode|ep|_)(?:-|)(\\d+)", RegexOptions.IgnoreCase);

        private static readonly WebClient Client = new WebClient();

        private readonly Uri watchLink;
        private readonly string seriesName;
        private readonly int updateTime;
        private Task task;

        public AnimuCrawlerBot(string link, string seriesName, int updateTime)
        {
            watchLink = new UriBuilder(link).Uri;
            this.seriesName = seriesName;
            this.updateTime = updateTime;
        }

        public void StartWatching()
        {
            if (task == null)
            {
                task = new Task(() =>
                {
                    while (true)
                    {
                        Crawl();
                        Thread.Sleep(updateTime);
                    }
                });
                task.Start();

            }
            else
            {
                task.Start();
            }
        }

        private void Crawl()
        {
            string webPage = Client.DownloadString(watchLink);
            MatchCollection links = UrlTagPattern.Matches(webPage);
            int i = 1;
            foreach (Match href in links)
            {
                string newUrl = HrefPattern.Match(href.Value).Groups[1].Value;
                Uri absoluteUrl = null;
                if (episodePattern.IsMatch(newUrl))
                {
                    Match episode = episodePattern.Match(newUrl);
                    string title = episode.Groups[1].ToString().Replace('-', ' ');
                    string noSpeTitle = Regex.Replace(title, @"[^0-9a-zA-Z]+", "");
                    string noSpeName = Regex.Replace(seriesName, @"[^0-9a-zA-Z]+", "");

                    if (noSpeTitle.ToLower().Contains(noSpeName.ToLower()))
                    {
                        Console.WriteLine(i++ + " " + title);
                    }

                    absoluteUrl = NormalizeUrl(watchLink, newUrl);
                }
                if (absoluteUrl != null  && (absoluteUrl.Scheme == Uri.UriSchemeHttp || absoluteUrl.Scheme == Uri.UriSchemeHttps))
                {

                }
            }
        }

        public void StopWatching()
        {
            task.Dispose();
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

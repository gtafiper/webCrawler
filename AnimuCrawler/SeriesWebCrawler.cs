using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SeriesCrawler
{
    public class SeriesWebCrawler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly string STATE_PAUSE = "Paused";
        private static readonly string STATE_RUNNING = "Running";
        private static readonly WebClient Client = new WebClient();

        private Thread thread;
        private Task task;
        private int id;
        private string status;
        private bool foundNew;
        private int updateTime;
        private Uri watchLink;
        private string seriesName;

        #region Proberties
        public List<Uri> Episodes { get; set; }

        public string Status
        {
            get { return status; }
            private set
            {
                if (value != this.status)
                {
                    this.status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool FoundNew
        {
            get { return foundNew; }
            private set
            {
                if (value != this.foundNew)
                {
                    this.foundNew = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int UpdateTime
        {
            get { return updateTime; }
        }

        public Uri WatchLink
        {
            get { return watchLink; }
        }

        public string SeriesName
        {
            get { return seriesName; }
        }

        public int ID
        {
            get { return id; }
        }
        #endregion

        internal SeriesWebCrawler(string link, string seriesName, int updateTime, int id)
        {
            this.watchLink = new UriBuilder(link).Uri;
            this.seriesName = seriesName;
            this.updateTime = updateTime;
            Episodes = new List<Uri>();
            foundNew = false;
            Status = STATE_PAUSE;
            this.id = id;
        }

        internal void StartWatching()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            if (task != null) {
                task.Dispose();
            }
            task = new Task(() =>
            {
                thread = Thread.CurrentThread;
                try
                {
                    while (true)
                    {
                        Crawl();

                        Thread.Sleep(UpdateTime);
                    }

                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e);
                    token.ThrowIfCancellationRequested();
                }
            });
            task.Start();
            Status = STATE_RUNNING;
        }

        internal void Seen()
        {
            FoundNew = false;
        }

        internal void StopWatching()
        {
            if (thread != null)
            {
                thread.Interrupt();
            }
            Status = STATE_PAUSE;
        }

        private void Crawl()
        {
            string webPage = Client.DownloadString(WatchLink);
            MatchCollection links = RegexPatterns.UrlTagPattern.Matches(webPage);
            foreach (Match href in links)
            {
                string newUrl = RegexPatterns.HrefPattern.Match(href.Value).Groups[1].Value;
                if (RegexPatterns.EpisodePattern.IsMatch(newUrl))
                {
                    HandleEpisode(newUrl);
                }
            }
        }

        private void HandleEpisode(string newUrl)
        {
            Match episode = RegexPatterns.EpisodePattern.Match(newUrl);

            string title = episode.Groups[1].ToString();

            string noSpeTitle = RegexPatterns.NonSpecialCharaterPattern.Replace(title, "");
            string noSpeName = RegexPatterns.NonSpecialCharaterPattern.Replace(SeriesName, "");

            if (noSpeTitle.ToLower().Contains(noSpeName.ToLower()))
            {
                Console.WriteLine(newUrl);
                Uri absoluteUrl = NormalizeUrl(watchLink, newUrl);
                if (!Episodes.Contains(absoluteUrl) && absoluteUrl != null &&
                    (absoluteUrl.Scheme == Uri.UriSchemeHttp || absoluteUrl.Scheme == Uri.UriSchemeHttps))
                {
                    Episodes.Add(absoluteUrl);
                    FoundNew = true;
                }
            }
        }

        private static Uri NormalizeUrl(Uri hostUrl, string url)
        {
            bool urlOk = Uri.TryCreate(hostUrl, url, out var absoluteUrl);
            if (urlOk)
            {
                return absoluteUrl.ToString().EndsWith("/") ? absoluteUrl : new UriBuilder(absoluteUrl + "/").Uri;
            }
            else
            {
                return null;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

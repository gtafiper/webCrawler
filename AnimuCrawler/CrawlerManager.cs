using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SeriesCrawler
{
    public class CrawlerManager : ICrawlerManager
    {
        private static CrawlerManager manager;

        private ObservableCollection<SeriesWebCrawler> crawlers;

        public ObservableCollection<SeriesWebCrawler> CrawlersRunning
        {
            get { return crawlers; }
            private set { crawlers = value; }
        }

        private CrawlerManager()
        {
            try
            {
                List<SeriesWebCrawler> temp = CrawlerFileHandler.GetAllBots();
                CrawlersRunning = new ObservableCollection<SeriesWebCrawler>(temp);
            }
            catch
            {
                CrawlersRunning = new ObservableCollection<SeriesWebCrawler>();
            }
        }

        public static CrawlerManager GetInstance()
        {
            if (manager == null)
            {
                manager = new CrawlerManager();
            }

            return manager;
        }

        public void AddBot(string watchLink, string title, int updateTime)
        {
            SeriesWebCrawler crawler = new SeriesWebCrawler(watchLink, title, updateTime, CreateUnigueID());
            CrawlerFileHandler.WriteNewBotToFile(crawler);
            CrawlersRunning.Add(crawler);
        }

        private int CreateUnigueID()
        {
            int id = 0;
            foreach (var item in CrawlersRunning)
            {
                if (item.ID > id)
                {
                    id = item.ID;
                }
            }

            return id + 1;
        }

        public void EndBot(SeriesWebCrawler active)
        {
            CrawlerFileHandler.SaveShows(active);
            active.StopWatching();
        }

        public void StartBot(SeriesWebCrawler active)
        {
            active.StartWatching();
        }

        public void MarkAsSeen(SeriesWebCrawler active)
        {
            active.Seen();
        }

        public void EndAllBots()
        {
            foreach (SeriesWebCrawler crawler in CrawlersRunning)
            {
                EndBot(crawler);
            }
        }

        public void RemoveBot(SeriesWebCrawler active)
        {
            active.StopWatching();
            CrawlerFileHandler.DeleteFile(active);
            CrawlersRunning.Remove(active);
        }
    }
}
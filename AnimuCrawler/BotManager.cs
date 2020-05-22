using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AnimuCrawler
{
    public class BotManager
    {
        private ObservableCollection<AnimuCrawlerBot> crawlers;
        public ObservableCollection<AnimuCrawlerBot> CrawlersRunning
        {
            get { return crawlers; }
            private set { crawlers = value; }
        }
        private static BotManager manager;

        private BotManager()
        {
            CrawlersRunning = new ObservableCollection<AnimuCrawlerBot>();
        }

        public static BotManager GetInstance()
        {
            if (manager == null)
            {
                manager = new BotManager();
            }
            return manager;
        }

        public void AddBot(string watchLink, string title)
        {
            AnimuCrawlerBot crawler = new AnimuCrawlerBot(watchLink, title, 50000);
            CrawlersRunning.Add(crawler);
        }

        private void EndAllBots()
        {
            foreach (AnimuCrawlerBot crawler in CrawlersRunning)
            {
                crawler.StopWatching();
            }
        }

        public void RemoveBot(AnimuCrawlerBot active)
        {
            CrawlersRunning.Remove(active);
        }
    }
}

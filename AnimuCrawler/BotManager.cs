using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace AnimuCrawler
{
    public class BotManager
    {
        private FileReader fileReader;
        private ObservableCollection<AnimuCrawlerBot> crawlers;
        public ObservableCollection<AnimuCrawlerBot> CrawlersRunning
        {
            get { return crawlers; }
            private set { crawlers = value; }
        }
        private static BotManager manager;


        private BotManager()
        {
            try {
                List<AnimuCrawlerBot> temp = fileReader.GetAllBots();
                CrawlersRunning = new ObservableCollection<AnimuCrawlerBot>(temp);
            } catch {
                CrawlersRunning = new ObservableCollection<AnimuCrawlerBot>();
            }
        }

        public static BotManager GetInstance()
        {
            if (manager == null)
            {
                manager = new BotManager();
            }

            return manager;
        }

        public void AddBot(string watchLink, string title, int updateTime)
        {
            AnimuCrawlerBot crawler = new AnimuCrawlerBot(watchLink, title, updateTime, title + updateTime);
            fileReader.WriteNewBotToFile(crawler);
            CrawlersRunning.Add(crawler);
        }

        public void EndBot(AnimuCrawlerBot active)
        {
            fileReader.SaveShows(active);
            active.StopWatching();
        }

        public void StartBot(AnimuCrawlerBot active) {
        }

        public void EndAllBots()
        {
            foreach (AnimuCrawlerBot crawler in CrawlersRunning)
            {
                crawler.StopWatching();
            }
        }

        public void RemoveBot(AnimuCrawlerBot active)
        {
            active.StopWatching();
            fileReader.DeleteFile(active);
            CrawlersRunning.Remove(active);
        }
    }
}

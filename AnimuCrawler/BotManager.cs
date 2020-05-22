using System.Collections.Generic;

namespace AnimuCrawler
{
    class BotManager
    {
        private FileReader fileReader;
        private List<AnimuCrawlerBot> CrawlersRunning { get; }
        private static BotManager manager;


        private BotManager()
        {
            CrawlersRunning = fileReader.GetAllBots();
        }

        private static BotManager GetInstance()
        {
            if (manager == null)
            {
                manager = new BotManager();
            }

            return manager;
        }

        public void AddBot(string watchLink, string title, string id)
        {
            AnimuCrawlerBot crawler = new AnimuCrawlerBot(watchLink, title, 50000, id);
            crawler.StartWatching();
            fileReader.WriteNewBotToFile(crawler);
            CrawlersRunning.Add(crawler);
        }

        private void EndBot(int index)
        {
            fileReader.SaveShows(CrawlersRunning[index]);
            CrawlersRunning[index].StopWatching();
        }

        private void EndAllBots()
        {
            foreach (AnimuCrawlerBot crawler in CrawlersRunning)
            {
                crawler.StopWatching();
            }
        }
    }
}
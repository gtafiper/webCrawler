﻿using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnimuCrawler
{
    class BotManager
    {
        private List<AnimuCrawlerBot> CrawlersRunning { get; }
        private static BotManager manager;

        private BotManager()
        {
            CrawlersRunning = new List<AnimuCrawlerBot>();
        }

        private static BotManager GetInstance()
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
            crawler.StartWatching();

            CrawlersRunning.Add(crawler);
        }

        private void EndBot() { }
        private void EndAllBots()
        {
            foreach (AnimuCrawlerBot crawler in CrawlersRunning)
            {
                crawler.StopWatching();
            }
        }
    }
}

using System.Collections.ObjectModel;

namespace SeriesCrawler
{
    public interface ICrawlerManager
    {
        ObservableCollection<SeriesWebCrawler> CrawlersRunning { get; }

        void AddBot(string watchLink, string title, int updateTime);
        void EndAllBots();
        void EndBot(SeriesWebCrawler active);
        void MarkAsSeen(SeriesWebCrawler active);
        void RemoveBot(SeriesWebCrawler active);
        void StartBot(SeriesWebCrawler active);
    }
}
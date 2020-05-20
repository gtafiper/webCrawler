using System;

namespace AnimuCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an URL: ");
            var urlStr = Console.ReadLine();

            AnimuCrawlerBot senpai = new AnimuCrawlerBot(urlStr);
            senpai.Watch();
            Console.ReadLine();
        }
    }
}

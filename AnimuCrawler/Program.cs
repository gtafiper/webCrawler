using System;

namespace AnimuCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an URL: ");
            var urlStr = Console.ReadLine(); 
            Console.Write("Enter an Name: ");
            var nameStr = Console.ReadLine();

            AnimuCrawlerBot senpai = new AnimuCrawlerBot(urlStr, nameStr);
            senpai.StartWatching();
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            AnimuCrawlerBot senpai = new AnimuCrawlerBot(urlStr, nameStr, 5000);
            senpai.StartWatching();
            Console.ReadLine();
            senpai.seen();
            Console.ReadLine();

        }
    }
}

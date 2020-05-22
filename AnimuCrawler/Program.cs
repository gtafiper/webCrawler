using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnimuCrawler
{
    class Program
    {
        private static FileReader fileReader = new FileReader();
        static void Main(string[] args)
        {

          

            /*Console.Write("Enter an URL: ");
            var urlStr = Console.ReadLine();
            Console.Write("Enter an Name: ");
            var nameStr = Console.ReadLine();*/

            //AnimuCrawlerBot senpai = new AnimuCrawlerBot(urlStr, nameStr, 5000, nameStr);
             Console.WriteLine(fileReader.GetAllBots());
             Console.ReadLine();
             /*senpai.StartWatching();
             Console.ReadLine();
             senpai.seen();
             Console.ReadLine();*/

        }
    }
}

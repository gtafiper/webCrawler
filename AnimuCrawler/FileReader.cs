using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnimuCrawler
{
    public class FileReader
    {
        public void WriteNewBotToFile(AnimuCrawlerBot crawler)
        {
            string path = @"bots\" + crawler.ID.ToString() + ".txt";
            using StreamWriter sw = File.AppendText(path);

            sw.WriteLine(crawler.WatchLink);
            sw.WriteLine(crawler.SeriesName);
            sw.WriteLine(crawler.UpdateTime);
            sw.WriteLine(crawler.ID);
            sw.Close();
        }


        public void SaveShows(AnimuCrawlerBot crawler)
        {
            string path = @"bots\" + crawler.ID.ToString() + ".txt";
            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine("episodeLinks:");
            foreach (var link in crawler.Episodes)
            {
                sw.WriteLine(link);
            }

            sw.Close();
        }

        public void DeleteFile(AnimuCrawlerBot crawler)
        {
            try
            {
                File.Delete(@"bots\" + crawler.ID.ToString() + ".txt");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public List<string> ReadLink(string path)
        {
            return File.ReadAllLines(path).Skip(5).ToList();
        }

        private string GetCrawlerUrl(string path)
        {
            return File.ReadAllLines(path).FirstOrDefault();
        }

        private string GetSeriesName(string path)
        {
            return File.ReadAllLines(path).Skip(1).FirstOrDefault();
        }

        private int GetUpdateTime(string path)
        {
            int updateTime;
            try
            {
                updateTime = int.Parse(File.ReadAllLines(path).Skip(2).FirstOrDefault() ?? throw new InvalidOperationException());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return updateTime;
        }

        private int GetCrawlerId(string path)
        {
            int id;
            try
            {

                id = int.Parse(File.ReadAllLines(path).Skip(3).FirstOrDefault() ?? throw new InvalidOperationException());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return id;
        }

        private string FindBotId(string id)
        {
            string path = @"bots\" + id + ".txt";
            foreach (var bot in File.ReadAllLines(path))
            {
                if (bot.Contains(id))
                {
                    return bot;
                }
            }

            return null;
        }

        private AnimuCrawlerBot CreateBotFromFile(string file)
        {
            AnimuCrawlerBot bot = new AnimuCrawlerBot(GetCrawlerUrl(file), GetSeriesName(file),
                GetUpdateTime(file), GetCrawlerId(file));

            return bot;
        }


        public List<AnimuCrawlerBot> GetAllBots()
        {
            List<AnimuCrawlerBot> bots = new List<AnimuCrawlerBot>();
            List<string> botFiles;
            try
            {
                botFiles = Directory.GetFiles(@"bots", "*").ToList();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }


            foreach (var bot in botFiles)
            {
                bots.Add(CreateBotFromFile(bot));
            }

            return bots;
        }
    }
}
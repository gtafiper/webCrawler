using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeriesCrawler
{
    public static class CrawlerFileHandler
    {
        public static void WriteNewBotToFile(SeriesWebCrawler crawler)
        {
            string path = @"bots\" + crawler.ID.ToString() + ".txt";
            if (!Directory.Exists("bots"))
            {
                Directory.CreateDirectory("bots");
            }

            using (StreamWriter sw = File.AppendText(path)){

                sw.WriteLine(crawler.WatchLink);
                sw.WriteLine(crawler.SeriesName);
                sw.WriteLine(crawler.UpdateTime);
                sw.WriteLine(crawler.ID);
            }
        }

        public static void SaveShows(SeriesWebCrawler crawler)
        {
            string path = @"bots\" + crawler.ID.ToString() + ".txt";

            if (!Directory.Exists("bots"))
            {
                Directory.CreateDirectory("bots");
            }

            var tempFile = Path.GetTempFileName();

            using (StreamWriter sw1 = File.AppendText(tempFile))
            {
                sw1.WriteLine(crawler.WatchLink);
                sw1.WriteLine(crawler.SeriesName);
                sw1.WriteLine(crawler.UpdateTime);
                sw1.WriteLine(crawler.ID);
                foreach (var link in crawler.Episodes)
                {
                    sw1.WriteLine(link);
                }
            }

            File.Delete(path);
            File.Move(tempFile, path);

        }

        public static  void DeleteFile(SeriesWebCrawler crawler)
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


        public static List<Uri> ReadLink(string path)
        {
            List<Uri> uris = new List<Uri>();
            List<string> lines =  File.ReadAllLines(path).Skip(4).ToList();
            foreach (var line in lines)
            {
                uris.Add(new UriBuilder(line).Uri);
            }

            return uris;
        }

        private static string GetCrawlerUrl(string path)
        {
            return File.ReadAllLines(path).FirstOrDefault();
        }

        private static string GetSeriesName(string path)
        {
            return File.ReadAllLines(path).Skip(1).FirstOrDefault();
        }

        private static int GetUpdateTime(string path)
        {
            int updateTime;
            try
            {
                updateTime = int.Parse(File.ReadAllLines(path).Skip(2).FirstOrDefault() ??
                                       throw new InvalidOperationException());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return updateTime;
        }

        private static int GetCrawlerId(string path)
        {
            int id;
            try
            {
                id = int.Parse(
                    File.ReadAllLines(path).Skip(3).FirstOrDefault() ?? throw new InvalidOperationException());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return id;
        }

        private static string FindBotId(string id)
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

        private static SeriesWebCrawler CreateBotFromFile(string file)
        {
            SeriesWebCrawler bot = new SeriesWebCrawler(GetCrawlerUrl(file), GetSeriesName(file),
                GetUpdateTime(file), GetCrawlerId(file));
            bot.Episodes = ReadLink(file);

            return bot;
        }


        public static List<SeriesWebCrawler> GetAllBots()
        {
            List<SeriesWebCrawler> bots = new List<SeriesWebCrawler>();
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
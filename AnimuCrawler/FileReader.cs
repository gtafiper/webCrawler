using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnimuCrawler
{
    public class FileReader
    {
        public void WriteShow(string show)
        {
            using StreamWriter sw = File.AppendText("shows.txt");

            sw.WriteLine(show);

            sw.Close();
        }


        public List<string> ReadShows()
        {
            try
            {
                List<string> list = File.ReadAllLines("shows.txt").ToList();

                return list;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            
        }

        public void DeleteShow(string show)
        {
            using StreamWriter sw = File.AppendText("shows.txt");
            List<string> lines = ReadShows();
            lines.Remove(show);
            foreach (var line in lines) 
            {
                sw.WriteLine(line);
            }
            sw.Close();
            
        }
    }
}
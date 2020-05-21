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
            var oldLines = System.IO.File.ReadAllLines("shows.txt");
            var newLines = oldLines.Where(line => !line.Contains(show));
            System.IO.File.WriteAllLines("shows.txt", newLines);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AnimuCrawler
{
    public static class RegexPatterns
    {
        public static readonly Regex UrlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);
        public static readonly Regex HrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
        public static readonly Regex episodePattern = new Regex("\\/([A-Za-z0-9_-]+)(?:episode|ep|_)(?:-|)(\\d+)", RegexOptions.IgnoreCase);
        public static readonly Regex nonSpecialCharaterPattern = new Regex(@"[^0-9a-zA-Z]+", RegexOptions.IgnoreCase);
    }
}

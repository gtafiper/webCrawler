using System.Text.RegularExpressions;

namespace SeriesCrawler
{
    internal static class RegexPatterns
    {
        internal static readonly Regex UrlTagPattern = new Regex(@"<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>", RegexOptions.IgnoreCase);

        internal static readonly Regex HrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);

        internal static readonly Regex EpisodePattern = new Regex("\\/([A-Za-z0-9_-]+)(?:episode|ep|_|-)(?:-|)(\\d+)", RegexOptions.IgnoreCase);

        internal static readonly Regex NonSpecialCharaterPattern = new Regex(@"[^0-9a-zA-Z]+", RegexOptions.IgnoreCase);
    }
}

using AngleSharp;
using AngleSharp.Dom;

namespace TKExtract
{
    internal class ScoreDownloader
    {
        private const string BASEURL = "http://triviakings.com/ajax/scores.php";
        private readonly string _venue;
        private readonly DateTimeOffset _date;
        private string _url => $"{BASEURL}?shortName={_venue}&gameDate={_date.ToString("yyyy-MM-dd")}";

        public ScoreDownloader(string venue, DateTimeOffset date)
        {
            _venue = venue;
            _date = date;
        }

        public async Task<IDocument> Download()
        {
            //https://triviakings.com/ajax/scores.php?shortName=pjskidoos&gameDate=2022-08-17
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var doc = await context.OpenAsync(_url);
            return doc;
        }
    }
}

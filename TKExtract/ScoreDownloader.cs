using AngleSharp;
using AngleSharp.Dom;

namespace TKExtract
{
    internal class ScoreDownloader
    {
        private const string BASEURL = "http://triviakings.com/ajax/scores.php";
        private readonly string Venue;
        private readonly DateTimeOffset Date;

        private string URL => $"{BASEURL}?shortName={this.Venue}&gameDate={this.Date.ToString("yyyy-MM-dd")}";

        public ScoreDownloader(string venue, DateTimeOffset date)
        {
            this.Venue = venue;
            this.Date = date;
        }

        public async Task<IDocument> Download()
        {
            //https://triviakings.com/ajax/scores.php?shortName=pjskidoos&gameDate=2022-08-17
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var doc = await context.OpenAsync(this.URL);
            return doc;
        }
    }
}

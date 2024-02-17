using AngleSharp;
using AngleSharp.Dom;

namespace TKExtract
{
    internal class VenueDownloader
    {
        private const string BASEURL = "https://triviakings.com/include/js/getShortNameJS.php";

        private string _url => BASEURL;

        public VenueDownloader()
        {
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

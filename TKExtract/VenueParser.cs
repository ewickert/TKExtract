using AngleSharp.Dom;
using System.Text.RegularExpressions;

namespace TKExtract
{
    internal class VenueParser
    {
        private IDocument _jsBlob;

        public VenueParser(IDocument jsBlob)
        {
            _jsBlob = jsBlob;
        }

        public IEnumerable<string> Parse()
        {
            List<string> names = new List<string>();
            var lines = _jsBlob.FirstChild.Text().Split('\n');
            var statements = lines[3].Split(';');
            foreach (var statement in statements)
            {
                var match = Regex.Match(statement, "'([a-z0-9]+)'");
                var name = match.Groups[1].Value;
                names.Add(name);
            }
            return names;
        }
    }
}

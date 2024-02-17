using AngleSharp.Dom;

namespace TKExtract
{
    internal class ScoreParser
    {
        private IDocument _scorePage;


        public ScoreParser(IDocument scorePage)
        {
            _scorePage = scorePage;
        }

        public ScoreSheet Parse()
        {
            var hostName = "NO GAME";
            var venue = "NO VENUE";
            hostName = _scorePage.QuerySelector(".title-small")?.Text().Split().Last() ?? hostName;
            var sheet = new ScoreSheet(hostName, venue);
            var rows = _scorePage.QuerySelectorAll(".table-row-data");
            foreach (var row in rows)
            {
                var offset = 1;

                var cells = row.QuerySelectorAll("td");
                var ordinal = cells[0].Text();
                var place = int.Parse(ordinal.Substring(0, ordinal.Length - 2));
                var teamName = cells[offset++].Text().Replace("\"", "'");
                offset++;
                List<ScorePeriod> periods = new List<ScorePeriod>();
                for (int x = 0; x < 3; x++)
                {
                    var round1 = int.Parse(cells[offset++].Text());
                    var round2 = int.Parse(cells[offset++].Text());
                    var wager9 = int.Parse(cells[offset++].Text());
                    var wager6 = int.Parse(cells[offset++].Text());
                    var wager3 = int.Parse(cells[offset++].Text());
                    periods.Add(new ScorePeriod(round1, round2, wager3, wager6, wager9));
                    offset += 3;
                }
                var finalWager = int.Parse(cells[offset++].Text());

                var r = new ScoreRow(teamName, finalWager, periods, place);
                sheet.AddRow(r);
            }
            return sheet;
        }
    }
}

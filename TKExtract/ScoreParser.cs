using AngleSharp.Dom;

namespace TKExtract
{
    internal class ScoreParser
    {
        private IDocument ScorePage;


        public ScoreParser(IDocument scorePage)
        {
            this.ScorePage = scorePage;
        }


        public ScoreSheet Parse()
        {
            var hostName = this.ScorePage.QuerySelector(".title-small").Text().Split().Last();
            var sheet = new ScoreSheet(hostName);
            var rows = this.ScorePage.QuerySelectorAll(".table-row-data");
            foreach (var row in rows)
            {
                var offset = 1;

                var cells = row.QuerySelectorAll("td");
                var teamName = cells[offset++].Text();
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

                var r = new ScoreRow(teamName, finalWager, periods);
                sheet.AddRow(r);
            }
            return sheet;
        }
    }
}

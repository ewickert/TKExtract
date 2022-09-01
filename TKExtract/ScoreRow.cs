namespace TKExtract
{
    internal class ScoreRow
    {
        public readonly string TeamName;
        public readonly ScorePeriod Period1;
        public readonly ScorePeriod Period2;
        public readonly ScorePeriod Period3;
        public readonly int FinalWager;

        public ScoreRow(string teamName, ScorePeriod period1, ScorePeriod period2, ScorePeriod period3, int finalWager)
        {
            TeamName = teamName;
            Period1 = period1;
            Period2 = period2;
            Period3 = period3;
            FinalWager = finalWager;
        }

        public ScoreRow(string teamName, int finalWager, List<ScorePeriod> periods) : this(teamName, periods[0], periods[1], periods[2], finalWager) { }

        public override string ToString()
        {
            return $"{TeamName} {Period1} {Period2} {Period3} {FinalWager}";
        }
    }
}

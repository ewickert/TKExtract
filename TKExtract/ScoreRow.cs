namespace TKExtract
{
    internal class ScoreRow
    {
        public readonly string TeamName;
        public readonly int Place;
        public readonly ScorePeriod Period1;
        public readonly ScorePeriod Period2;
        public readonly ScorePeriod Period3;
        public readonly int FinalWager;
        public int Total => Period1.Total + Period2.Total + FinalWager;

        public ScoreRow(string teamName, ScorePeriod period1, ScorePeriod period2, ScorePeriod period3, int finalWager, int place)
        {
            TeamName = teamName;
            Period1 = period1;
            Period2 = period2;
            Period3 = period3;
            FinalWager = finalWager;
            this.Place = place;
        }

        public ScoreRow(string teamName, int finalWager, List<ScorePeriod> periods, int place) : this(teamName, periods[0], periods[1], periods[2], finalWager, place) { }

        public override string ToString()
        {
            return $"{Place} {TeamName.Replace(' ', '_')} {Period1} {Period2} {Period3} {FinalWager}";
        }
    }
}

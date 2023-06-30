namespace TKExtract
{
    internal class ScoreRow
    {
        public readonly string TeamName;
        public readonly int Place;
        public readonly List<ScorePeriod> Periods;
        public readonly int FinalWager;
        public int Total => this.Periods.Sum(x => x.Total) + FinalWager;
        public int this[int round] => this.Periods[round / 5][round % 5];
        public ScoreRow(string teamName, int finalWager, List<ScorePeriod> periods, int place)
        {
            this.TeamName = teamName;
            this.FinalWager = finalWager;
            this.Periods = periods;
            this.Place = place;
        }

        public override string ToString()
        {
            return $"{Place} {TeamName.Replace(' ', '_')} {Periods[0]} {Periods[1]} {Periods[2]} {FinalWager}";
        }
    }
}

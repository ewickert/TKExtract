namespace TKExtract
{
    internal class ScorePeriod
    {
        public int[] Rounds = new int[5];

        public int Total => this.Rounds.Sum();
        public int this[int round] => this.Rounds[round];

        public ScorePeriod(int round1, int round2, int wager3, int wager6, int wager9)
        {
            Rounds[0] = round1;
            Rounds[1] = round2;
            Rounds[2] = wager3 * 3;
            Rounds[3] = wager6 * 6;
            Rounds[4] = wager9 * 9;
        }

        public override string ToString()
        {
            return String.Join(' ', this.Rounds);
        }

    }
}

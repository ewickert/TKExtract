namespace TKExtract
{
    internal class ScorePeriod
    {
        public readonly int Round1;
        public readonly int Round2;
        public readonly int Wager3;
        public readonly int Wager6;
        public readonly int Wager9;

        public int Total => Round1 + Round2 + Wager3 + Wager6 + Wager9;

        public ScorePeriod(int round1, int round2, int wager3, int wager6, int wager9)
        {
            Round1 = round1;
            Round2 = round2;
            Wager3 = wager3 * 3;
            Wager6 = wager6 * 6;
            Wager9 = wager9 * 9;
        }

        public override string ToString()
        {
            return $"{Round1} {Round2} {Wager3} {Wager6} {Wager9}";
        }

    }
}

using System.Collections;
using System.Text;

namespace TKExtract
{
    internal class ScoreSheet : IEnumerable<ScoreRow>
    {
        public readonly string HostName = "";
        public string Venue { get; set; }
        private readonly Dictionary<string, ScoreRow> Rows = new Dictionary<string, ScoreRow>();
        public ScoreRow this[string s] => this.Rows[s];
        public IEnumerable<string> Teams => this.Rows.Keys;
        public DateTimeOffset DatePlayed { get; set; }

        public ScoreSheet(string host, string venue)
        {
            this.HostName = host;
            this.Venue = venue;
        }


        public void AddRow(ScoreRow r)
        {
            this.Rows.Add(r.TeamName, r);
        }
        public bool TeamPlayed(string teamName)
        {
            return this.Rows.ContainsKey(teamName);
        }

        public IEnumerator<ScoreRow> GetEnumerator()
        {
            return ((IEnumerable<ScoreRow>)Rows.Values).GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.AppendLine(this.HostName);
            foreach (var row in Rows.Values)
            {
                sb.AppendLine($"{this.DatePlayed.ToString("yyyy-MM-dd")} {this.Venue} {this.HostName} {row.ToString()}");
            }
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Rows.Values).GetEnumerator();
        }
    }
}

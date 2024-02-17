using System.Collections;
using System.Text;

namespace TKExtract
{
    internal class ScoreSheet : IEnumerable<ScoreRow>
    {
        private readonly Dictionary<string, ScoreRow> _rows = new Dictionary<string, ScoreRow>();
        public readonly string HostName = "";
        public string Venue { get; set; }
        public ScoreRow this[string s] => _rows[s];
        public IEnumerable<string> Teams => _rows.Keys;
        public DateTimeOffset DatePlayed { get; set; }

        public ScoreSheet(string host, string venue)
        {
            HostName = host;
            Venue = venue;
        }


        public void AddRow(ScoreRow r)
        {
            _rows.Add(r.TeamName, r);
        }
        public bool TeamPlayed(string teamName)
        {
            return _rows.ContainsKey(teamName);
        }

        public IEnumerator<ScoreRow> GetEnumerator()
        {
            return ((IEnumerable<ScoreRow>)_rows.Values).GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.AppendLine(HostName);
            foreach (var row in _rows.Values)
            {
                sb.AppendLine($"{DatePlayed.ToString("yyyy-MM-dd")} {Venue} {HostName} {row.ToString()}");
            }
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rows.Values).GetEnumerator();
        }
    }
}

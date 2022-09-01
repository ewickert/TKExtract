using System.Text;

namespace TKExtract
{
    internal class ScoreSheet
    {
        public readonly string HostName;
        private List<ScoreRow> Rows = new List<ScoreRow>();

        public ScoreSheet(string host)
        {
            this.HostName = host;
        }

        public void AddRow(ScoreRow r)
        {
            this.Rows.Add(r);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.HostName);
            foreach (var row in Rows)
            {
                sb.AppendLine(row.ToString());
            }
            return sb.ToString();
        }
    }
}

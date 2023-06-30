using System.Text;

namespace TKExtract
{
    internal class SummaryGenerator
    {
        private readonly List<ScoreSheet> Sheets = new List<ScoreSheet>();

        private List<string> TeamNames = new List<string>();
        public void AddSheet(ScoreSheet s)
        {
            this.Sheets.Add(s);
            foreach (var name in s.Teams)
            {
                if (!this.TeamNames.Contains(name)) this.TeamNames.Add(name);
            }
        }

        public string GenerateScoreSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DATE ");
            foreach (var team in TeamNames)
            {
                sb.Append(team.Replace(' ', '_') + " ");
            }
            foreach (var sheet in Sheets)
            {
                sb.AppendLine();
                sb.Append(sheet.DatePlayed.ToString("yyyy-MM-dd") + " ");
                foreach (var name in TeamNames)
                {
                    if (sheet.TeamPlayed(name))
                        sb.Append(sheet[name].Total);
                    else sb.Append("-");

                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }
        public string GeneratePlaceSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DATE ");
            foreach (var team in TeamNames)
            {
                sb.Append(team.Replace(' ', '_') + " ");
            }
            foreach (var sheet in Sheets)
            {
                sb.AppendLine();
                sb.Append(sheet.DatePlayed.ToString("yyyy-MM-dd") + " ");
                foreach (var name in TeamNames)
                {
                    if (sheet.TeamPlayed(name))
                        sb.Append(sheet[name].Place);
                    else sb.Append("_");

                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }

        public string GenerateRoundDetail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("DATE ");
            sb.Append("ROUND ");
            foreach (var team in TeamNames)
            {
                sb.Append($"{team.Replace(' ', '_')} ");
            }
            sb.AppendLine();
            foreach (var sheet in Sheets)
            {
                for (int x = 0; x < 15; x++)
                {
                    sb.Append($"{sheet.DatePlayed.ToString("yyyy-MM-dd")} {x + 1} ");
                    foreach (var team in TeamNames)
                    {
                        if (sheet.TeamPlayed(team))
                        {
                            var row = sheet[team];
                            sb.Append($"{row[x]} ");
                        }
                        else
                            sb.Append("- ");
                    }
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

    }
}

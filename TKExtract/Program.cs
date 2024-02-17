using CommandDotNet;
using CommandDotNet.NameCasing;
using CommandDotNet.Spectre;
using Spectre.Console;

namespace TKExtract
{
    public class Program
    {
        private const string DEFAULTVENUE = "pjskidoos";
        static int Main(string[] args)
        {
            return new AppRunner<Program>()
                 .UseNameCasing(Case.LowerCase)
                 .UseSpectreAnsiConsole()
                 .Run(args);

        }

        [Command(Description = "Extract scores from TriviaKings")]
        public void Scores(IAnsiConsole stdOut, string? venue, DateTimeOffset? start, DateTimeOffset? end)
        {

            venue ??= DEFAULTVENUE;
            var cDate = start ?? DateTimeOffset.Now;
            var eDate = end ?? DateTimeOffset.Now;
            cDate = FindLastWednesday(cDate);
            eDate = FindLastWednesday(eDate);
            do
            {
                var dl = new ScoreDownloader(venue, cDate);
                var sheet = new ScoreParser(dl.Download().Result).Parse();
                if (sheet.Count() == 0)
                {
                    cDate = cDate.AddDays(7);
                    continue;
                }
                sheet.Venue = venue;
                sheet.DatePlayed = cDate;
                stdOut.Write(sheet.ToString());
                cDate = cDate.AddDays(7);
            }
            while (cDate <= eDate);
        }

        [Command(Description = "Extract events from TriviaKings")]
        public void Calendar(IAnsiConsole stdOut, DateTimeOffset? start, DateTimeOffset? end, string? venue)
        {
            var cDate = start ?? DateTimeOffset.Now;
            var eDate = end ?? DateTimeOffset.Now.AddDays(7);
            var cal = new Calendar(cDate, eDate);
            var events = cal.GetEvents().Result;
            foreach (var e in events)
            {
                if (venue is not null && e.ShortName != venue)
                    continue;
                stdOut.WriteLine(e.ToString());
            }
        }

        static DateTimeOffset FindLastWednesday(DateTimeOffset MaybeWednesday)
        {
            while (MaybeWednesday.DayOfWeek != DayOfWeek.Wednesday)
            {
                MaybeWednesday = MaybeWednesday.AddDays(-1);
            }

            return MaybeWednesday;
        }
    }
}
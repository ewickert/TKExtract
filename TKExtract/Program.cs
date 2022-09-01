using McMaster.Extensions.CommandLineUtils;

namespace TKExtract
{
    internal class Program
    {
        private const string DEFAULTVENUE = "pjskidoos";
        static void Main(string[] args)
        {

            var defaultDate = FindLastWednesday(DateTimeOffset.Now);

            var app = new CommandLineApplication();
            app.HelpOption("-h|-?|--help");
            var venue = app.Option("-v|--venue <venue>", "Venue for which to search results", CommandOptionType.SingleValue);
            venue.DefaultValue = DEFAULTVENUE;

            var startDate = app.Option("-s|--start <date>", "Date to search from", CommandOptionType.SingleValue);
            startDate.DefaultValue = defaultDate.ToString("yyyy-MM-dd");


            var endDate = app.Option("-e|--end <date>", "Date to search to", CommandOptionType.SingleValue);
            endDate.DefaultValue = defaultDate.ToString("yyyy-MM-dd");

            var output = app.Option("-o|--output", "Pattern for output files", CommandOptionType.SingleValue);
            output.DefaultValue = String.Empty;


            app.OnExecute(() =>
            {
                if (!DateTimeOffset.TryParse(startDate.Value(), out var sDate))
                {
                    app.ShowHelp();
                    Environment.Exit(1);
                }
                if (!DateTimeOffset.TryParse(endDate.Value(), out var eDate))
                {
                    app.ShowHelp();
                    Environment.Exit(2);
                }

                sDate = FindLastWednesday(sDate);
                eDate = FindLastWednesday(eDate);
                var cDate = sDate;

                do
                {
                    var dl = new ScoreDownloader(venue.Value(), cDate);
                    var sheet = new ScoreParser(dl.Download().Result).Parse();
                    if (string.IsNullOrWhiteSpace(output.Value()))
                    {
                        Console.Write(sheet.ToString());
                        Console.WriteLine();
                    }
                    else
                    {
                        File.AppendAllText(string.Format(output.Value(), cDate), sheet.ToString());
                    }
                    cDate = cDate.AddDays(7);
                }
                while (cDate <= eDate);
            });

            app.Execute(args);
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
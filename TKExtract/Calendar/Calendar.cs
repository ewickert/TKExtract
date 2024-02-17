using AngleSharp.Io;
using Newtonsoft.Json;

namespace TKExtract;

public class Calendar
{
    //start = time_t, end=time_t
    private const string BASEURL = "https://triviakings.com/calendar/hostingCalendarEventFeed.php";
    private readonly DateTimeOffset _start;
    private readonly DateTimeOffset _end;
    private IEnumerable<TriviaEvent>? _events;
    private string _url => $"{BASEURL}?start={_start.ToUnixTimeSeconds()}&end={_end.ToUnixTimeSeconds()}";
    private HttpClient _http = new HttpClient();

    public Calendar(DateTimeOffset start, DateTimeOffset end)
    {
        _start = start;
        _end = end;
    }

    public async Task<IEnumerable<TriviaEvent>> GetEvents()
    {
        await Fetch();
        return _events;
    }
    private async Task Fetch()
    {
        using var response = await _http.GetAsync(_url);
        response.EnsureSuccessStatusCode();
        var results = await response.Content.ReadAsStringAsync();
        _events = JsonConvert.DeserializeObject<IEnumerable<TriviaEvent>>(results);
    }
}

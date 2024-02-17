namespace TKExtract;

public class TriviaEvent
{
    public string Title { get; set; }
    public DateTimeOffset Start { get; set; }
    public string Hosts { get; set; }
    public string TriviaEventTIme { get; set; }
    public bool IsCancelled { get; set; }
    public string URL { get; set; }
    public int LocationID { get; set; }
    public string ShortName { get; set; }
    public override string ToString()
    {
        return $"{Start} {Title} {Hosts} {(IsCancelled ? "Cancelled" : "Scheduled")}";
    }
}

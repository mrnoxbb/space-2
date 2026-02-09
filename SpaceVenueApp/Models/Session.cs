namespace SpaceVenueApp.Models;

public class Session
{
    public int Id { get; set; }
    public string StationName { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal? Cost { get; set; }
}

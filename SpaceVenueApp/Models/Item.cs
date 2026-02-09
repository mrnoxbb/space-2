namespace SpaceVenueApp.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int UnitsSold { get; set; }
    public decimal Revenue { get; set; }
}

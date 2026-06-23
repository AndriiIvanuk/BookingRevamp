namespace BookingRevamp.Models;

public class PropertyImage
{
    public int Id { get; set; }

    public string ImagePath { get; set; }

    public int PropertyId { get; set; }

    public Property Property { get; set; }

    public string FileName { get; set; }
}

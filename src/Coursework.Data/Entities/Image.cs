namespace Coursework.Data.Entities;

public class Image
{
    public Guid Id { get; set; }

    public string Path { get; set; }
    
    public bool IsMain { get; set; }
    
    public Guid PlaceId { get; set; }
    
    public Place Place { get; set; }
}
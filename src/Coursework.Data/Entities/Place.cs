namespace Coursework.Data.Entities;

public class Place
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string ShortDescription { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public decimal Longitude { get; set; }
    
    public decimal Latitude { get; set; }
    
    public District District { get; set; }
    
    public Type Type { get; set; }

    public List<Image> Images { get; set; }
}
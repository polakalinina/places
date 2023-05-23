namespace Coursework.Data.Entities;

public class District
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } 
    
    public List<Place> Places { get; set; }
}
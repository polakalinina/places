namespace Coursework.Core.Models;

public class UpdatePlaceModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string ShortDescription { get; set; }
    
    public decimal Longitude { get; set; }
    
    public decimal Latitude { get; set; }
}
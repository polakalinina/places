using Microsoft.AspNetCore.Http;

namespace Coursework.Core.Models;

public class CreatePlaceModel
{
    public string Name { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Coordinates { get; set; }
    
    public Guid DistrictId { get; set; }
    
    public Guid TypeId { get; set; }
    
    public List<IFormFile> Images { get; set; }
}
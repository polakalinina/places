namespace Coursework.Data.Entities;

public class Type
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public Guid LogoId { get; set; }

    public TypeLogo Logo { get; set; }
    
    public List<Place> Places { get; set; }
}
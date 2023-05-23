using System.Globalization;
using Coursework.Core.Models;
using Coursework.Data;
using Coursework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Core.Services;

public class PlacesService
{
    private readonly ApplicationContext _context;

    public PlacesService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Place>> Get()
    {
        var places = await _context
            .Places
            .Include(place => place.District)
            .Include(place => place.Images)
            .ToListAsync();

        return places;
    }
    
    public async Task<List<Place>> GetWithFilters(Guid? districtId, Guid? typeId)
    {
        if (districtId is null && typeId is null)
        {
            return await Get();
        }
        
        var places = await _context
            .Places
            .Include(place => place.District)
            .Include(place => place.Images)
            .Where(place => (districtId == null || place.District.Id == districtId) &&
                            (typeId == null || place.Type.Id == typeId))
            .ToListAsync();

        return places;
    }
    
    public async Task<Place> Get(Guid placeId)
    {
        var place = await _context
            .Places
            .Include(place => place.District)
            .Include(place => place.Type)
            .Include(place => place.Images)
            .FirstOrDefaultAsync(place => place.Id == placeId);

        return place;
    }

    public async Task<List<Place>> GetClosePlaces(Guid placeId)
    {
        var place = await Get(placeId);

        var closePlaces = await _context
            .Places
            .Include(otherPlace => otherPlace.Images)
            .Where(otherPlace => otherPlace.Id != placeId)
            .OrderBy(otherPlace => (place.Latitude - otherPlace.Latitude) * (place.Latitude - otherPlace.Latitude) +
                                   (place.Longitude - otherPlace.Longitude) * (place.Longitude - otherPlace.Longitude))
            .Take(3)
            .ToListAsync();

        return closePlaces;
    }

    public async Task<Guid> Create(CreatePlaceModel model)
    {
        var coordinates = model.Coordinates.Split(" ");
        var latitude = decimal.Parse(coordinates[0], CultureInfo.InvariantCulture);
        var longitude = decimal.Parse(coordinates[1], CultureInfo.InvariantCulture);
        
        var type = await _context.Types.FirstAsync(type => type.Id == model.TypeId);
        var district = await _context.Districts.FirstAsync(district => district.Id == model.DistrictId);

        var place = new Place
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            ShortDescription = model.ShortDescription,
            LastUpdate = DateTime.UtcNow,
            Longitude = longitude,
            Latitude = latitude,
            Type = type,
            District = district
        };
        _context.Places.Add(place);

        if (model.Images is not null && model.Images.Any())
        {
            var images = new List<Image>();
            foreach (var imageFile in model.Images)
            {
                var imageId = Guid.NewGuid();
                var fileType = imageFile.FileName.Split(".").Last();
                var filePath = $"place-img/{imageId}.{fileType}";
                var image = new Image
                {
                    Id = imageId,
                    Path = $"./../{filePath}",
                    IsMain = false,
                    PlaceId = place.Id
                };
                images.Add(image);

                await using var stream = File.Create($"wwwroot/{filePath}");
                await imageFile.CopyToAsync(stream);
            }
            images.First().IsMain = true;
            
            _context.Images.AddRange(images);
        }
        
        await _context.SaveChangesAsync();

        return place.Id;
    }
    
    public async Task Update(UpdatePlaceModel model)
    {
        var place = await Get(model.Id);

        place.Name = model.Name;
        place.ShortDescription = model.ShortDescription;
        place.Longitude = model.Longitude;
        place.Latitude = model.Latitude;

        _context.Places.Update(place);
        await _context.SaveChangesAsync();
    }
}
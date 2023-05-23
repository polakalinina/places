using Coursework.Data;
using Coursework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Core.Services;

public class ImagesService
{
    private readonly ApplicationContext _context;

    public ImagesService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Image>> Get(Guid placeId)
    {
        var images = await _context.Images.Where(image => image.PlaceId == placeId).ToListAsync();

        return images;
    }
}
using Coursework.Data;
using Coursework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Core.Services;

public class DistrictsService
{
    private readonly ApplicationContext _context;

    public DistrictsService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<District>> Get()
    {
        var places = await _context
            .Districts
            .OrderBy(district => district.Name)
            .ToListAsync();

        return places;
    }
}
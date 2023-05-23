using Coursework.Data;
using Microsoft.EntityFrameworkCore;
using Type = Coursework.Data.Entities.Type;

namespace Coursework.Core.Services;

public class TypesService
{
    private readonly ApplicationContext _context;

    public TypesService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<List<Type>> Get()
    {
        var types = await _context
            .Types
            .OrderBy(type => type.Name)
            .ToListAsync();

        return types;
    }
    
    public async Task<List<Type>> GetWithPlaces()
    {
        var types = await _context
            .Types
            .Include(type => type.Places)
            .Include(type => type.Logo)
            .OrderBy(type => type.Name)
            .ToListAsync();

        return types;
    }
}
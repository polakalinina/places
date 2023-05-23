using Coursework.Core.Services;
using Coursework.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Type = Coursework.Data.Entities.Type;

namespace Coursework.Web.Pages.Places;

public class Index : PageModel
{
    private readonly PlacesService _placesService;
    private readonly DistrictsService _districtsService;
    private readonly TypesService _typesService;

    public Index(PlacesService placesService, DistrictsService districtsService, TypesService typesService)
    {
        _placesService = placesService;
        _districtsService = districtsService;
        _typesService = typesService;
    }
    
    public List<Place> Places { get; set; }
    public List<District> Districts { get; set; }
    public List<Type> Types { get; set; }

    public District ChoosenDistrict { get; set; }
    public Type ChoosenType { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Places = await _placesService.Get();
        Districts = await _districtsService.Get();
        Types = await _typesService.Get();

        return Page();
    }

    public async Task<IActionResult> OnPost(Guid? districtId, Guid? typeId)
    {
        Places = await _placesService.GetWithFilters(districtId, typeId);
        Districts = await _districtsService.Get();
        Types = await _typesService.Get();

        ChoosenDistrict = Districts.FirstOrDefault(district => district.Id == districtId);
        ChoosenType = Types.FirstOrDefault(type => type.Id == typeId);

        return Page();
    }
}
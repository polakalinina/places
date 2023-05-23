using Coursework.Core.Models;
using Coursework.Core.Services;
using Coursework.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Type = Coursework.Data.Entities.Type;

namespace Coursework.Web.Pages.Places;

public class Create : PageModel
{
    private readonly PlacesService _placesService;
    private readonly TypesService _typesService;
    private readonly DistrictsService _districtsService;
    
    public Create(PlacesService placesService, TypesService typesService, DistrictsService districtsService)
    {
        _placesService = placesService;
        _typesService = typesService;
        _districtsService = districtsService;
    }
    
    public List<Type> Types { get; set; }
    public List<District> Districts { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Types = await _typesService.Get();
        Districts = await _districtsService.Get();
        
        return Page();
    }

    public async Task<IActionResult> OnPost(CreatePlaceModel model)
    {
        var placeId = await _placesService.Create(model);
        
        return RedirectToPage("Details", new { placeId });
    }
}
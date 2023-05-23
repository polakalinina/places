using Coursework.Core.Services;
using Coursework.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coursework.Web.Pages.Places;

public class Details : PageModel
{
    private readonly PlacesService _placesService;
    
    public Details(PlacesService placesService)
    {
        _placesService = placesService;
    }
    
    public Place Place { get; set; }
    public List<Place> ClosePlaces { get; set; }

    public async Task<IActionResult> OnGet(Guid placeId)
    {
        Place = await _placesService.Get(placeId);
        ClosePlaces = await _placesService.GetClosePlaces(placeId);
        
        return Page(); 
    }
}
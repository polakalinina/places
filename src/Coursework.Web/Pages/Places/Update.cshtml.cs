using Coursework.Core.Models;
using Coursework.Core.Services;
using Coursework.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coursework.Web.Pages.Places;

public class Update : PageModel
{
    private readonly PlacesService _placesService;
    
    public Update(PlacesService placesService)
    {
        _placesService = placesService;
    }
    
    public Place Place { get; set; }
    
    public async Task<IActionResult> OnGet(Guid placeId)
    {
        Place = await _placesService.Get(placeId);

        return Page();
    }

    public async Task<IActionResult> OnPost(UpdatePlaceModel model)
    {
        await _placesService.Update(model);
        
        return RedirectToPage("Details", model.Id);
    }
}
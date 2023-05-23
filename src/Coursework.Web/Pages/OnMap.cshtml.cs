using Coursework.Core.Services;
using Coursework.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Coursework.Web.Pages;

public class OnMap : PageModel
{
    private PlacesService _placesService;

    public OnMap(PlacesService placesService)
    {
        _placesService = placesService;
    }

    public List<Place> Places { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Places = await _placesService.Get();
        
        return Page();
    }
}
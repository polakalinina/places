using Coursework.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Type = Coursework.Data.Entities.Type;

namespace Coursework.Web.Pages;

public class IndexModel : PageModel
{
    private readonly TypesService _typesService;

    public IndexModel(TypesService typesService)
    {
        _typesService = typesService;
    }

    public List<Type> Types { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Types = await _typesService.GetWithPlaces();

        return Page();
    }
}
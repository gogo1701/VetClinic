using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Owners;

public class DetailsModel : PageModel
{
    private readonly IOwnerService _ownerService;

    public DetailsModel(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public Owner? Owner { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Owner = await _ownerService.GetOwnerByIdAsync(id);
        
        if (Owner == null)
        {
            return NotFound();
        }

        return Page();
    }
}
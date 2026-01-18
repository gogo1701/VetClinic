using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Pets;

public class DetailsModel : PageModel
{
    private readonly IPetService _petService;

    public DetailsModel(IPetService petService)
    {
        _petService = petService;
    }

    public Pet? Pet { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Pet = await _petService.GetPetByIdAsync(id);
        
        if (Pet == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _petService.DeletePetAsync(id);
        TempData["SuccessMessage"] = "Pet has been deleted successfully.";
        return RedirectToPage("Index");
    }
}
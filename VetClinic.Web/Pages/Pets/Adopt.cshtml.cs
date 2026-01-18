    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Pets;

public class AdoptModel : PageModel
{
    private readonly IPetService _petService;
    private readonly IOwnerService _ownerService;

    public AdoptModel(IPetService petService, IOwnerService ownerService)
    {
        _petService = petService;
        _ownerService = ownerService;
    }

    public Pet? Pet { get; set; }
    public SelectList Owners { get; set; } = null!;

    [BindProperty]
    public int? SelectedOwnerId { get; set; }

    [BindProperty]
    public NewOwnerModel NewOwner { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Pet = await _petService.GetPetByIdAsync(id);
        if (Pet == null)
        {
            return NotFound();
        }

        await LoadOwnersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        Pet = await _petService.GetPetByIdAsync(id);
        if (Pet == null)
        {
            return NotFound();
        }

        int ownerId;

        if (SelectedOwnerId.HasValue && SelectedOwnerId.Value > 0)
        {
            ownerId = SelectedOwnerId.Value;
        }
        else if (!string.IsNullOrWhiteSpace(NewOwner.FirstName) && !string.IsNullOrWhiteSpace(NewOwner.LastName))
        {
            var owner = new Owner
            {
                FirstName = NewOwner.FirstName,
                LastName = NewOwner.LastName,
                Email = NewOwner.Email ?? "",
                Phone = NewOwner.Phone ?? "",
                Address = NewOwner.Address
            };
            await _ownerService.CreateOwnerAsync(owner);
            ownerId = owner.Id;
        }
        else
        {
            ModelState.AddModelError("", "Please select an existing owner or enter new owner details.");
            await LoadOwnersAsync();
            return Page();
        }

        await _petService.MarkAsAdoptedAsync(id, ownerId);
        
        TempData["SuccessMessage"] = $"Congratulations! {Pet.Name} has been adopted!";
        return RedirectToPage("Details", new { id });
    }

    private async Task LoadOwnersAsync()
    {
        var owners = await _ownerService.GetAllOwnersAsync();
        Owners = new SelectList(owners, "Id", "FullName");
    }

    public class NewOwnerModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
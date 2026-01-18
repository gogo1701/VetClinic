using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Pets;

public class EditModel : PageModel
{
    private readonly IPetService _petService;
    private readonly IOwnerService _ownerService;

    public EditModel(IPetService petService, IOwnerService ownerService)
    {
        _petService = petService;
        _ownerService = ownerService;
    }

    [BindProperty]
    public PetEditModel Pet { get; set; } = new();
    
    public SelectList Owners { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var pet = await _petService.GetPetByIdAsync(id);
        if (pet == null)
        {
            return NotFound();
        }

        Pet = new PetEditModel
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            DateOfBirth = pet.DateOfBirth,
            Color = pet.Color,
            Weight = pet.Weight,
            MicrochipNumber = pet.MicrochipNumber,
            OwnerId = pet.OwnerId
        };

        await LoadOwnersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadOwnersAsync();
            return Page();
        }

        var existingPet = await _petService.GetPetByIdAsync(Pet.Id);
        if (existingPet == null)
        {
            return NotFound();
        }

        existingPet.Name = Pet.Name;
        existingPet.Species = Pet.Species;
        existingPet.Breed = Pet.Breed;
        existingPet.DateOfBirth = Pet.DateOfBirth;
        existingPet.Color = Pet.Color;
        existingPet.Weight = Pet.Weight;
        existingPet.MicrochipNumber = Pet.MicrochipNumber;
        existingPet.OwnerId = Pet.OwnerId;
        existingPet.IsAdopted = Pet.OwnerId.HasValue;

        await _petService.UpdatePetAsync(existingPet);
        
        TempData["SuccessMessage"] = $"Pet '{existingPet.Name}' has been updated successfully!";
        return RedirectToPage("Details", new { id = Pet.Id });
    }

    private async Task LoadOwnersAsync()
    {
        var owners = await _ownerService.GetAllOwnersAsync();
        Owners = new SelectList(owners, "Id", "FullName");
    }

    public class PetEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Species { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string? Color { get; set; }

        [Range(0.1, 500)]
        public decimal Weight { get; set; }

        public string? MicrochipNumber { get; set; }
        
        public int? OwnerId { get; set; }
    }
}
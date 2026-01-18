using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Pets;

public class CreateModel : PageModel
{
    private readonly IPetService _petService;
    private readonly IOwnerService _ownerService;

    public CreateModel(IPetService petService, IOwnerService ownerService)
    {
        _petService = petService;
        _ownerService = ownerService;
    }

    [BindProperty]
    public PetInputModel Pet { get; set; } = new();
    
    public SelectList Owners { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadOwnersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadOwnersAsync();
            return Page();
        }

        var pet = new Pet
        {
            Name = Pet.Name,
            Species = Pet.Species,
            Breed = Pet.Breed,
            DateOfBirth = Pet.DateOfBirth,
            Color = Pet.Color,
            Weight = Pet.Weight,
            MicrochipNumber = Pet.MicrochipNumber,
            OwnerId = Pet.OwnerId,
            IsAdopted = Pet.OwnerId.HasValue
        };

        await _petService.CreatePetAsync(pet);
        
        TempData["SuccessMessage"] = $"Pet '{pet.Name}' has been added successfully!";
        return RedirectToPage("Index");
    }

    private async Task LoadOwnersAsync()
    {
        var owners = await _ownerService.GetAllOwnersAsync();
        Owners = new SelectList(owners, "Id", "FullName");
    }

    public class PetInputModel
    {
        [Required(ErrorMessage = "Pet name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Species is required")]
        public string Species { get; set; } = string.Empty;

        [Required(ErrorMessage = "Breed is required")]
        [StringLength(100)]
        public string Breed { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-1);

        public string? Color { get; set; }

        [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kg")]
        public decimal Weight { get; set; }

        public string? MicrochipNumber { get; set; }
        
        public int? OwnerId { get; set; }
    }
}
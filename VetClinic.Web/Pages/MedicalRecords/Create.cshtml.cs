using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.MedicalRecords;

public class CreateModel : PageModel
{
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IPetService _petService;

    public CreateModel(IMedicalRecordService medicalRecordService, IPetService petService)
    {
        _medicalRecordService = medicalRecordService;
        _petService = petService;
    }

    [BindProperty]
    public MedicalRecordInputModel Record { get; set; } = new();

    public SelectList Pets { get; set; } = null!;

    public async Task OnGetAsync(int? petId)
    {
        await LoadPetsAsync();
        Record.VisitDate = DateTime.Today;
        
        if (petId.HasValue)
        {
            Record.PetId = petId.Value;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadPetsAsync();
            return Page();
        }

        var record = new MedicalRecord
        {
            PetId = Record.PetId,
            VisitDate = Record.VisitDate,
            Diagnosis = Record.Diagnosis,
            Treatment = Record.Treatment,
            Medications = Record.Medications,
            Cost = Record.Cost,
            VetNotes = Record.VetNotes
        };

        await _medicalRecordService.CreateAsync(record);

        TempData["SuccessMessage"] = "Medical record added successfully!";
        return RedirectToPage("Index");
    }

    private async Task LoadPetsAsync()
    {
        var pets = await _petService.GetAllPetsAsync();
        Pets = new SelectList(
            pets.Select(p => new { p.Id, Display = $"{p.Name} ({p.Species} - {p.Owner?.FullName ?? "No owner"})" }),
            "Id", "Display");
    }

    public class MedicalRecordInputModel
    {
        [Required(ErrorMessage = "Please select a pet")]
        [Display(Name = "Pet")]
        public int PetId { get; set; }

        [Required(ErrorMessage = "Visit date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        [StringLength(1000)]
        public string Diagnosis { get; set; } = string.Empty;

        public string? Treatment { get; set; }

        public string? Medications { get; set; }

        [Range(0, 100000, ErrorMessage = "Cost must be a positive value")]
        public decimal? Cost { get; set; }

        [Display(Name = "Vet Notes")]
        public string? VetNotes { get; set; }
    }
}
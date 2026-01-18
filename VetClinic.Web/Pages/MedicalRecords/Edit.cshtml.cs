using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.MedicalRecords;

public class EditModel : PageModel
{
    private readonly IMedicalRecordService _medicalRecordService;

    public EditModel(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    [BindProperty]
    public MedicalRecordEditModel Record { get; set; } = new();

    public string PetName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var record = await _medicalRecordService.GetByIdAsync(id);
        if (record == null)
        {
            return NotFound();
        }

        PetName = $"{record.Pet.Name} ({record.Pet.Species})";
        Record = new MedicalRecordEditModel
        {
            Id = record.Id,
            PetId = record.PetId,
            VisitDate = record.VisitDate,
            Diagnosis = record.Diagnosis,
            Treatment = record.Treatment,
            Medications = record.Medications,
            Cost = record.Cost,
            VetNotes = record.VetNotes
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var existingRecord = await _medicalRecordService.GetByIdAsync(Record.Id);
            PetName = existingRecord != null ? $"{existingRecord.Pet.Name} ({existingRecord.Pet.Species})" : "";
            return Page();
        }

        var record = await _medicalRecordService.GetByIdAsync(Record.Id);
        if (record == null)
        {
            return NotFound();
        }

        record.VisitDate = Record.VisitDate;
        record.Diagnosis = Record.Diagnosis;
        record.Treatment = Record.Treatment;
        record.Medications = Record.Medications;
        record.Cost = Record.Cost;
        record.VetNotes = Record.VetNotes;

        await _medicalRecordService.UpdateAsync(record);

        TempData["SuccessMessage"] = "Medical record updated successfully!";
        return RedirectToPage("Details", new { id = Record.Id });
    }

    public class MedicalRecordEditModel
    {
        public int Id { get; set; }
        public int PetId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Diagnosis { get; set; } = string.Empty;

        public string? Treatment { get; set; }
        public string? Medications { get; set; }

        [Range(0, 100000)]
        public decimal? Cost { get; set; }

        public string? VetNotes { get; set; }
    }
}
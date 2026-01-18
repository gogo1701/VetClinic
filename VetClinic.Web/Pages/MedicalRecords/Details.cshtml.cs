using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.MedicalRecords;

public class DetailsModel : PageModel
{
    private readonly IMedicalRecordService _medicalRecordService;

    public DetailsModel(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    public MedicalRecord? Record { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Record = await _medicalRecordService.GetByIdAsync(id);

        if (Record == null)
        {
            return NotFound();
        }

        return Page();
    }
}
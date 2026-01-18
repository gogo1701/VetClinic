using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.MedicalRecords;

public class IndexModel : PageModel
{
    private readonly IMedicalRecordService _medicalRecordService;

    public IndexModel(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    public IEnumerable<MedicalRecord> MedicalRecords { get; set; } = [];
    public string? SearchTerm { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public async Task OnGetAsync(string? searchTerm, DateTime? fromDate, DateTime? toDate)
    {
        SearchTerm = searchTerm;
        FromDate = fromDate;
        ToDate = toDate;

        var records = await _medicalRecordService.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            records = records.Where(r => 
                r.Pet.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                r.Diagnosis.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                (r.Treatment?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        if (fromDate.HasValue)
        {
            records = records.Where(r => r.VisitDate >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            records = records.Where(r => r.VisitDate <= toDate.Value);
        }

        MedicalRecords = records.ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _medicalRecordService.DeleteAsync(id);
        TempData["SuccessMessage"] = "Medical record deleted successfully.";
        return RedirectToPage();
    }
}
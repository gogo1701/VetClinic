using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Appointments;

public class CreateModel : PageModel
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPetService _petService;

    public CreateModel(IAppointmentService appointmentService, IPetService petService)
    {
        _appointmentService = appointmentService;
        _petService = petService;
    }

    [BindProperty]
    public AppointmentInputModel Appointment { get; set; } = new();
    
    public SelectList Pets { get; set; } = null!;

    public async Task OnGetAsync(int? petId)
    {
        await LoadPetsAsync();
        
        if (petId.HasValue)
        {
            Appointment.PetId = petId.Value;
        }
        
        Appointment.ScheduledDate = DateTime.Today.AddDays(1).AddHours(9);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadPetsAsync();
            return Page();
        }

        var appointment = new Appointment
        {
            PetId = Appointment.PetId,
            ScheduledDate = Appointment.ScheduledDate,
            Reason = Appointment.Reason,
            Notes = Appointment.Notes,
            Status = AppointmentStatus.Scheduled
        };

        await _appointmentService.CreateAppointmentAsync(appointment);
        
        TempData["SuccessMessage"] = "Appointment scheduled successfully!";
        return RedirectToPage("Index");
    }

    private async Task LoadPetsAsync()
    {
        var pets = await _petService.GetAllPetsAsync();
        Pets = new SelectList(pets.Select(p => new { p.Id, Display = $"{p.Name} ({p.Species} - {p.Owner?.FullName ?? "No owner"})" }), "Id", "Display");
    }

    public class AppointmentInputModel
    {
        [Required(ErrorMessage = "Please select a pet")]
        [Display(Name = "Pet")]
        public int PetId { get; set; }

        [Required(ErrorMessage = "Please select a date and time")]
        [Display(Name = "Date & Time")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Please select a reason")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}
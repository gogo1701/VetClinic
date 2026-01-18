using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;

namespace VetClinic.Web.Pages.Appointments;

public class IndexModel : PageModel
{
    private readonly IAppointmentService _appointmentService;

    public IndexModel(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public IEnumerable<Appointment> UpcomingAppointments { get; set; } = [];
    public IEnumerable<Appointment> AllAppointments { get; set; } = [];

    public async Task OnGetAsync()
    {
        UpcomingAppointments = await _appointmentService.GetUpcomingAppointmentsAsync();
        AllAppointments = await _appointmentService.GetAllAppointmentsAsync();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int id, AppointmentStatus status)
    {
        await _appointmentService.UpdateStatusAsync(id, status);
        return RedirectToPage();
    }
}
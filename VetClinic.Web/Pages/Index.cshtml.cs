using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IPetService _petService;
    private readonly IOwnerService _ownerService;
    private readonly IAppointmentService _appointmentService;

    public IndexModel(IPetService petService, IOwnerService ownerService, IAppointmentService appointmentService)
    {
        _petService = petService;
        _ownerService = ownerService;
        _appointmentService = appointmentService;
    }

    public int TotalPets { get; set; }
    public int AdoptedPets { get; set; }
    public int AvailablePets { get; set; }
    public int TotalOwners { get; set; }
    public int TodayAppointments { get; set; }
    public IEnumerable<Pet> RecentPets { get; set; } = [];
    public IEnumerable<Appointment> UpcomingAppointments { get; set; } = [];

    public async Task OnGetAsync()
    {
        var allPets = await _petService.GetAllPetsAsync();
        TotalPets = allPets.Count();
        AdoptedPets = allPets.Count(p => p.IsAdopted);
        AvailablePets = allPets.Count(p => !p.IsAdopted);
        RecentPets = allPets.Take(4);

        var allOwners = await _ownerService.GetAllOwnersAsync();
        TotalOwners = allOwners.Count();

        var todayAppts = await _appointmentService.GetAppointmentsByDateAsync(DateTime.Today);
        TodayAppointments = todayAppts.Count();

        UpcomingAppointments = await _appointmentService.GetUpcomingAppointmentsAsync();
    }
}
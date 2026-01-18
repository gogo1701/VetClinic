using VetClinic.Web.Models;

namespace VetClinic.Web.Services;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync();
    Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date);
    Task<Appointment?> GetAppointmentByIdAsync(int id);
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    Task UpdateAppointmentAsync(Appointment appointment);
    Task UpdateStatusAsync(int id, AppointmentStatus status);
    Task DeleteAppointmentAsync(int id);
}
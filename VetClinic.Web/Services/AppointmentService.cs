using Microsoft.EntityFrameworkCore;
using VetClinic.Web.Data;
using VetClinic.Web.Models;

namespace VetClinic.Web.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .ThenInclude(p => p.Owner)
            .OrderByDescending(a => a.ScheduledDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync()
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .ThenInclude(p => p.Owner)
            .Where(a => a.ScheduledDate >= DateTime.Today && a.Status != AppointmentStatus.Cancelled)
            .OrderBy(a => a.ScheduledDate)
            .Take(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByDateAsync(DateTime date)
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .ThenInclude(p => p.Owner)
            .Where(a => a.ScheduledDate.Date == date.Date)
            .OrderBy(a => a.ScheduledDate)
            .ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .ThenInclude(p => p.Owner)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        appointment.CreatedAt = DateTime.UtcNow;
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task UpdateAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id, AppointmentStatus status)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            appointment.Status = status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
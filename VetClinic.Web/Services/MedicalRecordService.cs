using Microsoft.EntityFrameworkCore;
using VetClinic.Web.Data;
using VetClinic.Web.Models;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Services;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly ApplicationDbContext _context;

    public MedicalRecordService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        return await _context.MedicalRecords
            .Include(m => m.Pet)
            .ThenInclude(p => p.Owner)
            .OrderByDescending(m => m.VisitDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<MedicalRecord>> GetByPetIdAsync(int petId)
    {
        return await _context.MedicalRecords
            .Include(m => m.Pet)
            .Where(m => m.PetId == petId)
            .OrderByDescending(m => m.VisitDate)
            .ToListAsync();
    }

    public async Task<MedicalRecord?> GetByIdAsync(int id)
    {
        return await _context.MedicalRecords
            .Include(m => m.Pet)
            .ThenInclude(p => p.Owner)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MedicalRecord> CreateAsync(MedicalRecord record)
    {
        record.CreatedAt = DateTime.UtcNow;
        _context.MedicalRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task UpdateAsync(MedicalRecord record)
    {
        _context.MedicalRecords.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var record = await _context.MedicalRecords.FindAsync(id);
        if (record != null)
        {
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }
    }
}
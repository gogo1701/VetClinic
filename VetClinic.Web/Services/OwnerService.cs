using Microsoft.EntityFrameworkCore;
using VetClinic.Web.Data;
using VetClinic.Web.Models;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Services;

public class OwnerService : IOwnerService
{
    private readonly ApplicationDbContext _context;

    public OwnerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
    {
        return await _context.Owners
            .Include(o => o.Pets)
            .OrderBy(o => o.LastName)
            .ThenBy(o => o.FirstName)
            .ToListAsync();
    }

    public async Task<Owner?> GetOwnerByIdAsync(int id)
    {
        return await _context.Owners
            .Include(o => o.Pets)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Owner> CreateOwnerAsync(Owner owner)
    {
        owner.CreatedAt = DateTime.UtcNow;
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();
        return owner;
    }

    public async Task UpdateOwnerAsync(Owner owner)
    {
        _context.Owners.Update(owner);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOwnerAsync(int id)
    {
        var owner = await _context.Owners.FindAsync(id);
        if (owner != null)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
        }
    }
}
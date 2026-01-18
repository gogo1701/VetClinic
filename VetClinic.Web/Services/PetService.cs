using Microsoft.EntityFrameworkCore;
using VetClinic.Web.Data;
using VetClinic.Web.Models;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Services;

public class PetService : IPetService
{
    private readonly ApplicationDbContext _context;

    public PetService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pet>> GetAllPetsAsync()
    {
        return await _context.Pets
            .Include(p => p.Owner)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Pet?> GetPetByIdAsync(int id)
    {
        return await _context.Pets
            .Include(p => p.Owner)
            .Include(p => p.Appointments.OrderByDescending(a => a.ScheduledDate))
            .Include(p => p.MedicalRecords.OrderByDescending(m => m.VisitDate))
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pet>> GetPetsForAdoptionAsync()
    {
        return await _context.Pets
            .Where(p => !p.IsAdopted)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Pet>> SearchPetsAsync(string? searchTerm, string? species, bool? isAdopted)
    {
        var query = _context.Pets.Include(p => p.Owner).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Name.Contains(searchTerm) ||
                                     p.Breed.Contains(searchTerm) ||
                                     (p.Owner != null && (p.Owner.FirstName.Contains(searchTerm) ||
                                                          p.Owner.LastName.Contains(searchTerm))));
        }

        if (!string.IsNullOrWhiteSpace(species))
        {
            query = query.Where(p => p.Species == species);
        }

        if (isAdopted.HasValue)
        {
            query = query.Where(p => p.IsAdopted == isAdopted.Value);
        }

        return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<Pet> CreatePetAsync(Pet pet)
    {
        pet.CreatedAt = DateTime.UtcNow;
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task UpdatePetAsync(Pet pet)
    {
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePetAsync(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet != null)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MarkAsAdoptedAsync(int petId, int ownerId)
    {
        var pet = await _context.Pets.FindAsync(petId);
        if (pet != null)
        {
            pet.IsAdopted = true;
            pet.AdoptionDate = DateTime.UtcNow;
            pet.OwnerId = ownerId;
            await _context.SaveChangesAsync();
        }
    }
}
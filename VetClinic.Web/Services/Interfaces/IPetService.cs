using VetClinic.Web.Models;

namespace VetClinic.Web.Services.Interfaces;

public interface IPetService
{
    Task<IEnumerable<Pet>> GetAllPetsAsync();
    Task<Pet?> GetPetByIdAsync(int id);
    Task<IEnumerable<Pet>> GetPetsForAdoptionAsync();
    Task<IEnumerable<Pet>> SearchPetsAsync(string? searchTerm, string? species, bool? isAdopted);
    Task<Pet> CreatePetAsync(Pet pet);
    Task UpdatePetAsync(Pet pet);
    Task DeletePetAsync(int id);
    Task MarkAsAdoptedAsync(int petId, int ownerId);
}
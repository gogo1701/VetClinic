using VetClinic.Web.Models;

namespace VetClinic.Web.Services.Interfaces;

public interface IOwnerService
{
    Task<IEnumerable<Owner>> GetAllOwnersAsync();
    Task<Owner?> GetOwnerByIdAsync(int id);
    Task<Owner> CreateOwnerAsync(Owner owner);
    Task UpdateOwnerAsync(Owner owner);
    Task DeleteOwnerAsync(int id);
}
using VetClinic.Web.Models;

namespace VetClinic.Web.Services.Interfaces;

public interface IMedicalRecordService
{
    Task<IEnumerable<MedicalRecord>> GetAllAsync();
    Task<IEnumerable<MedicalRecord>> GetByPetIdAsync(int petId);
    Task<MedicalRecord?> GetByIdAsync(int id);
    Task<MedicalRecord> CreateAsync(MedicalRecord record);
    Task UpdateAsync(MedicalRecord record);
    Task DeleteAsync(int id);
}
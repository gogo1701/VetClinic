namespace VetClinic.Web.Models;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Color { get; set; }
    public decimal Weight { get; set; }
    public string? MicrochipNumber { get; set; }
    public bool IsAdopted { get; set; }
    public DateTime? AdoptionDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public int? OwnerId { get; set; }
    public Owner? Owner { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
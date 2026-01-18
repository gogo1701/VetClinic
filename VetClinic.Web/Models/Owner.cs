namespace VetClinic.Web.Models;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string FullName => $"{FirstName} {LastName}";
    
    // Navigation properties
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
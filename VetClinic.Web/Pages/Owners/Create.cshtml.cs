using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Owners;

public class CreateModel : PageModel
{
    private readonly IOwnerService _ownerService;

    public CreateModel(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [BindProperty]
    public OwnerInputModel Owner { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var owner = new Owner
        {
            FirstName = Owner.FirstName,
            LastName = Owner.LastName,
            Email = Owner.Email,
            Phone = Owner.Phone,
            Address = Owner.Address
        };

        await _ownerService.CreateOwnerAsync(owner);
        
        TempData["SuccessMessage"] = $"Owner '{owner.FullName}' has been added successfully!";
        return RedirectToPage("Index");
    }

    public class OwnerInputModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        public string? Address { get; set; }
    }
}
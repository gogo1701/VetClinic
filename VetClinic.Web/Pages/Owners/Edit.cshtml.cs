using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Owners;

public class EditModel : PageModel
{
    private readonly IOwnerService _ownerService;

    public EditModel(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [BindProperty]
    public OwnerEditModel Owner { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var owner = await _ownerService.GetOwnerByIdAsync(id);
        if (owner == null)
        {
            return NotFound();
        }

        Owner = new OwnerEditModel
        {
            Id = owner.Id,
            FirstName = owner.FirstName,
            LastName = owner.LastName,
            Email = owner.Email,
            Phone = owner.Phone,
            Address = owner.Address
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var owner = await _ownerService.GetOwnerByIdAsync(Owner.Id);
        if (owner == null)
        {
            return NotFound();
        }

        owner.FirstName = Owner.FirstName;
        owner.LastName = Owner.LastName;
        owner.Email = Owner.Email;
        owner.Phone = Owner.Phone;
        owner.Address = Owner.Address;

        await _ownerService.UpdateOwnerAsync(owner);

        TempData["SuccessMessage"] = $"Owner '{owner.FullName}' updated successfully!";
        return RedirectToPage("Details", new { id = Owner.Id });
    }

    public class OwnerEditModel
    {
        public int Id { get; set; }

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
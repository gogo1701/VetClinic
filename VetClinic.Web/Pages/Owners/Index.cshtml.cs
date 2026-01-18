using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Owners;

public class IndexModel : PageModel
{
    private readonly IOwnerService _ownerService;

    public IndexModel(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public IEnumerable<Owner> Owners { get; set; } = [];

    public async Task OnGetAsync()
    {
        Owners = await _ownerService.GetAllOwnersAsync();
    }
}
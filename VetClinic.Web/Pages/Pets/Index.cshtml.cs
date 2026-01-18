using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Web.Models;
using VetClinic.Web.Services;
using VetClinic.Web.Services.Interfaces;

namespace VetClinic.Web.Pages.Pets;

public class IndexModel : PageModel
{
    private readonly IPetService _petService;

    public IndexModel(IPetService petService)
    {
        _petService = petService;
    }

    public IEnumerable<Pet> Pets { get; set; } = [];
    public string? SearchTerm { get; set; }
    public string? Species { get; set; }
    public bool? IsAdopted { get; set; }
    
    public int TotalPets { get; set; }
    public int AdoptedPets { get; set; }
    public int AvailablePets { get; set; }
    public int UniqueSpecies { get; set; }

    public async Task OnGetAsync(string? searchTerm, string? species, bool? isAdopted)
    {
        SearchTerm = searchTerm;
        Species = species;
        IsAdopted = isAdopted;

        Pets = await _petService.SearchPetsAsync(searchTerm, species, isAdopted);
        
        var allPets = await _petService.GetAllPetsAsync();
        TotalPets = allPets.Count();
        AdoptedPets = allPets.Count(p => p.IsAdopted);
        AvailablePets = allPets.Count(p => !p.IsAdopted);
        UniqueSpecies = allPets.Select(p => p.Species).Distinct().Count();
    }
}
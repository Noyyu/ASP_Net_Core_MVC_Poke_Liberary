using ASP_Net_Core_MVC_Liberary.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Net_Core_MVC_Liberary.Controllers
{
    public class PokemonController : Controller
    {
        //Items
        private readonly IPokemonService _pokemonService;

        //Construct
        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }
        //List of pokemons
        public async Task<IActionResult> Index()
        {
            var pokemonList = await _pokemonService.GetAllPokemonAsync();
            return View(pokemonList);
        }

    }
}

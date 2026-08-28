using ASP_Net_Core_MVC_Liberary.Models;
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
            if (pokemonList == null)
            {
                return NotFound();
            }
            return View(pokemonList);
        }
        public async Task<IActionResult> Details(int Id)
        {
            var pokemonDetails = await _pokemonService.GetPokemonDetailsAsync(Id);
            if(pokemonDetails == null)
            {
                return NotFound();
            }
            return View(pokemonDetails);
        }

        public async Task<IActionResult> Search(string search)
        {
            var pokemonFound = await _pokemonService.GetPokemon(search);
            if (pokemonFound == null)
            {
                return NotFound();
            }
            return View("index", pokemonFound);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMore(int offset)
        {
            var pokemons = await _pokemonService.GetMorePokemonAsync(offset);
            return PartialView("_PokemonCardList", pokemons);
        }

    }
}

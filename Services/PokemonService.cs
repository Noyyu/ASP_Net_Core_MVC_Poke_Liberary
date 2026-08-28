using ASP_Net_Core_MVC_Liberary.Models;
using Microsoft.Extensions.Caching.Memory;
namespace ASP_Net_Core_MVC_Liberary.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonService> _logger;
        private readonly IMemoryCache _cache;

        public PokemonService(HttpClient httpClient, ILogger<PokemonService> logger, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _cache = cache;
        }


        //Actions
        public async Task<List<PokemonListItem>?> GetAllPokemonAsync()
        {
            try
            {
                //Should return a list with pokemon of 151 items. 
                string url = "https://pokeapi.co/api/v2/pokemon?limit=151";
                var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>(url);
                return response?.Results;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not get a list.");
                return null;
            }
        }

        public async Task<List<PokemonListItem>?> GetMorePokemonAsync(int offset)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit=20";
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>(url);
            return response?.Results;
        }
        public async Task<PokemonDetails?> GetPokemonDetailsAsync(int Id)
        {
            try
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{Id}";
                var response = await _httpClient.GetFromJsonAsync<PokemonDetails>(url);
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not get the pokemon details");
                return null;
            }
        }

        // 1. Ändra returtypen till List<PokemonListItem>
        public async Task<List<PokemonListItem>?> GetPokemon(string search)
        {
            //If the user enters nothing
            if (search == null)
            {
                var list = await GetAllPokemonAsync();
                return list;
            }

            try
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{search}";
                var response = await _httpClient.GetFromJsonAsync<SinglePokemonResponse>(url);

                // 2. Gör listan till en List<PokemonListItem>
                var pokemonList = new List<PokemonListItem> {
                    new PokemonListItem
                    {
                        Name = response?.Name ?? "Unknown",
                        Url = $"https://pokeapi.co/api/v2/pokemon/{response?.Id}/"
                    }
                };
                return pokemonList;
            }
            //If the pokemon could not be found..
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Could not find the pokemon, tries to find simmular..");

                var allPokemon  = await GetCachedPokemonListAsync();
                if (allPokemon == null) return null;

                var similarPokemons = allPokemon.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                    & p.Id > 0 & p.Id < 10000).ToList();

                return similarPokemons;
            }
        }

        private async Task<List<PokemonListItem>> GetCachedPokemonListAsync()
        {
            return await _cache.GetOrCreateAsync("AllPokemon", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                string url = "https://pokeapi.co/api/v2/pokemon?limit=10000";
                var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>(url);
                return response?.Results ?? new List<PokemonListItem>();
            });
        }
    }
}

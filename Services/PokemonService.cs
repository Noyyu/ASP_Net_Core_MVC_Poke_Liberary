using ASP_Net_Core_MVC_Liberary.Models;

namespace ASP_Net_Core_MVC_Liberary.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(HttpClient httpClient, ILogger<PokemonService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
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
    }
}

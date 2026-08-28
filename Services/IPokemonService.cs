using ASP_Net_Core_MVC_Liberary.Models;

namespace ASP_Net_Core_MVC_Liberary.Services
{
    public interface IPokemonService
    {
        Task<List<PokemonListItem>?> GetAllPokemonAsync();
        Task<PokemonDetails?> GetPokemonDetailsAsync(int Id);
        Task<List<PokemonListItem>?> GetPokemon(string search);
        Task<List<PokemonListItem>?> GetMorePokemonAsync(int offset);
    }
}
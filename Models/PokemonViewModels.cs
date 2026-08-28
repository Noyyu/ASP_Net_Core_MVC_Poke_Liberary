using System.ComponentModel.DataAnnotations;

namespace ASP_Net_Core_MVC_Liberary.Models
{
    public class PokemonListResponse
    {
        public List<PokemonListItem> Results { get; set; }
    }

    public class PokemonListItem
    {
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }

        public int Id
        {
            //Calculates the ID from the URL automatically
            get
            {
                if (string.IsNullOrEmpty(Url)) return 0;
                var segments = Url.TrimEnd('/').Split('/');
                return int.TryParse(segments[^1], out var id) ? id : 0;
            }
            set;
        }
    }

    public class SinglePokemonResponse
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Id { get; set; }
    }

    public class PokemonDetails
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Id { get; set; }
        public List<PokemonType>? Types { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
    }

    public class PokemonType
    {
        [Required]
        public int Slot { get; set; }
        public PokemonResourceItem? Type { get; set; }
    }

    public class PokemonResourceItem //The API is build to use resource items for all kinds of items, getting name and url. 
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Url { get; set; } = string.Empty;
    }
}

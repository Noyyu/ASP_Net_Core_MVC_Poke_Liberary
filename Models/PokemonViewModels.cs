namespace ASP_Net_Core_MVC_Liberary.Models
{
    public class PokemonListResponse
    {
        public List<PokemonListItem> Results { get; set; }
    }

    public class PokemonListItem
    {
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
        }
    }

    public class PokemonDetails
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; //Fix
        public int Height { get; set; }
        public int Weight { get; set; }
    }

}

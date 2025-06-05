namespace BreweryAPI.Entities
{
    public class Brewery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Beer> Beers { get; set; } = new List<Beer>();
    }
}

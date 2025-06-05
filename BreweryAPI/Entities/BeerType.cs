namespace BreweryAPI.Entities
{
    public class BeerType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public List<Beer> Beers { get; set; } = new List<Beer>();
    }
}

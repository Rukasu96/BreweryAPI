namespace BreweryAPI.Entities
{
    public class Brewery : UserAccount
    {
        public List<Beer> Beers { get; set; } = new List<Beer>();
    }
}

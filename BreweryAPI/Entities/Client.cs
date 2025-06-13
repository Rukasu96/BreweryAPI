namespace BreweryAPI.Entities
{
    public class Client : UserAccount
    {
        public List<Beer> Beers { get; set; }
    }
}

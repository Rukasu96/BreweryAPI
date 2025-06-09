namespace BreweryAPI.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BeerType Type { get; set; }
        public int IBUPercentage { get; set; }
        public decimal StrongValue { get; set; }
        public decimal Price { get; set; }
        public Brewery Brewery { get; set; }
        public Guid BreweryId { get; set; }
        public List<Wholesaler> Wholesalers { get; set; }
    }
}

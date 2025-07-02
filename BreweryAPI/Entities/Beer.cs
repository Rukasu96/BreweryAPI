namespace BreweryAPI.Entities
{
    public enum BeerType
    {
        Pils,
        Lager
    }

    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IBUPercentage { get; set; }
        public decimal StrongValue { get; set; }
        public BeerType Type { get; set; }
        public decimal Price { get; set; }
        public Brewery Brewery { get; set; }
        public Guid BreweryId { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<ShopBasket> ShopBaskets { get; set; }
    }
}

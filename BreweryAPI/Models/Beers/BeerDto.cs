namespace BreweryAPI.Models.Beers
{
    public class BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int IBUPercentage { get; set; }
        public decimal StrongValue { get; set; }
        public decimal Price { get; set; }
        public string Brewery { get; set; }
    }
}

using BreweryAPI.Entities;

namespace BreweryAPI.Models.Beers
{
    public class CreatedBeerDto
    {
        public string Name { get; set; }
        public int IBUPercentage { get; set; }
        public decimal StrongValue { get; set; }
        public BeerType Type { get; set; }
        public decimal Price { get; set; }
    }
}

namespace BreweryAPI.Models
{
    public class BeerUpdateDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int IBUPercentage { get; set; }
        public decimal StrongValue { get; set; }
        public decimal Price { get; set; }
    }
}

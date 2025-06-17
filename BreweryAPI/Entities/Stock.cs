namespace BreweryAPI.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public Beer BeerInStock { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public CompanyAccount CompanyAccount { get; set; }
        public Guid CompanyAccountId { get; set; }
    }
}

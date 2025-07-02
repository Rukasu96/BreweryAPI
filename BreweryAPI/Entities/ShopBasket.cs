namespace BreweryAPI.Entities
{
    public class ShopBasket
    {
        public int Id { get; set; }
        public Beer BeerInBasket { get; set; }
        public int BeerInBasketId { get; set; }
        public int Quantity { get; set; }
        public Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}

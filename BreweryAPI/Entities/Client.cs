namespace BreweryAPI.Entities
{
    public class Client : UserAccount
    {
        public List<ShopBasket> ShopBaskets { get; set; }
        public int ShopBasketId { get; set; }
    }
}

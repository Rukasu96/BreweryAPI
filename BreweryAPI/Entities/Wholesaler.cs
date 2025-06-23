namespace BreweryAPI.Entities
{
    public class Wholesaler : CompanyAccount
    {
        public List<ShopBasket> ShopBaskets { get; set; }
    }
}

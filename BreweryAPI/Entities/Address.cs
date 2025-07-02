namespace BreweryAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public UserAccount UserAccount { get; set; }
        public Guid UserAccountId { get; set; }
    }
}

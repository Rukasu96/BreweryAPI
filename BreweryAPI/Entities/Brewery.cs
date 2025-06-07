namespace BreweryAPI.Entities
{
    public class Brewery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public List<Beer> Beers { get; set; } = new List<Beer>();
        public Address Address { get; set; }
    }
}

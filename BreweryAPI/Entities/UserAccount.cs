namespace BreweryAPI.Entities
{
    public class UserAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public Address Address { get; set; }
    }
}

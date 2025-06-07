using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.Entities
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {
            
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<BeerType> BeerTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}

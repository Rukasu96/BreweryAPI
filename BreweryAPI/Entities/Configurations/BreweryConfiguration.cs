using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class BreweryConfiguration : IEntityTypeConfiguration<Brewery>
    {
        public void Configure(EntityTypeBuilder<Brewery> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(12)").IsRequired();

            builder.HasMany(x => x.Beers).WithOne(x => x.Brewery).HasForeignKey(x => x.BreweryId);
            builder.HasOne(x => x.Address).WithOne(x => x.Brewery).HasForeignKey<Address>(x => x.BreweryId);
        }
    }
}

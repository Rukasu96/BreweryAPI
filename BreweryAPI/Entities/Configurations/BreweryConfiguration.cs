using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class BreweryConfiguration : IEntityTypeConfiguration<Brewery>
    {
        public void Configure(EntityTypeBuilder<Brewery> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.HasMany(x => x.Beers).WithOne(x => x.Brewery).HasForeignKey(x => x.BreweryId);
        }
    }
}

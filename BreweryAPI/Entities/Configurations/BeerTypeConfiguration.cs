using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class BeerTypeConfiguration : IEntityTypeConfiguration<BeerType>
    {
        public void Configure(EntityTypeBuilder<BeerType> builder)
        {
            builder.Property(x => x.TypeName).HasColumnName("Type").HasColumnType("varchar(10)").IsRequired();
        }
    }
}

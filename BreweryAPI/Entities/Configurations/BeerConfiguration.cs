using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class BeerConfiguration : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.IBUPercentage).HasColumnName("IBU").IsRequired();
            builder.Property(x => x.StrongValue).HasColumnName("Alc.").HasColumnType("decimal(3,1)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(5,2)").IsRequired();

            builder.HasOne(x => x.BeerType);
        }
    }
}

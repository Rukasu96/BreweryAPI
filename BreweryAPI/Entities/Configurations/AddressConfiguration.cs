using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.City).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Street).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PostalCode).HasColumnType("varchar(50)").IsRequired();
        }
    }
}

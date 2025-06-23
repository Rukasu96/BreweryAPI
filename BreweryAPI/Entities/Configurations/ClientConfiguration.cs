using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(12)").IsRequired();

            builder.HasMany(x => x.ShopBaskets).WithOne(x => x.Client).HasForeignKey(x => x.ClientId);
        }
    }
}

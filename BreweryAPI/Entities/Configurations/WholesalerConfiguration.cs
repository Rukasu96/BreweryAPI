using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class WholesalerConfiguration : IEntityTypeConfiguration<Wholesaler>
    {
        public void Configure(EntityTypeBuilder<Wholesaler> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(12)").IsRequired();

            builder.HasMany(x => x.Beers).WithMany(x => x.Wholesalers).UsingEntity<BeerWholesaler>(
                x => x.HasOne(bw => bw.Beer).WithMany().HasForeignKey(bw => bw.BeerId),
                x => x.HasOne(bw => bw.Wholesaler).WithMany().HasForeignKey(bw => bw.WholesalerId).OnDelete(DeleteBehavior.ClientCascade),

                bw =>
                {
                    bw.HasKey(x => new { x.BeerId, x.WholesalerId });
                    bw.Property(x => x.AddedDate).HasDefaultValueSql("getutcdate()");
                }
                );
        }
    }
}

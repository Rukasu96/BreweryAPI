using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class ShopBasketConfiguration : IEntityTypeConfiguration<ShopBasket>
    {
        public void Configure(EntityTypeBuilder<ShopBasket> builder)
        {
            builder.HasOne(x => x.BeerInBasket).WithMany(x => x.ShopBaskets).HasForeignKey(x => x.BeerInBasketId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Client).WithMany(x => x.ShopBaskets).HasForeignKey(x => x.ClientId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class ShopBasketConfiguration : IEntityTypeConfiguration<ShopBasket>
    {
        public void Configure(EntityTypeBuilder<ShopBasket> builder)
        {
            builder.HasOne(x => x.BeerInBasket).WithMany().HasForeignKey(x => x.BeerId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

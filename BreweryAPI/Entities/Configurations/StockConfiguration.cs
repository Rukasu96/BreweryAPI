using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasOne(x => x.BeerInStock).WithMany(x => x.Stocks).HasForeignKey(x => x.BeerId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.CompanyAccount).WithMany(x => x.Stocks).HasForeignKey(x => x.CompanyAccountId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

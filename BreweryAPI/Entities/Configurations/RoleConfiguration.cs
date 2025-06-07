using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreweryAPI.Entities.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.RoleName).HasColumnType("varchar(20)").HasColumnName("Role").IsRequired();
            builder.HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Brewery"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Wholesaler"
                },
                new Role
                {
                    Id = 3,
                    RoleName = "Client"
                });
        }
    }
}

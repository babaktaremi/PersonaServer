using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.IdentityStoresConfigurations;

internal class RoleClaimConfig:IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.ToTable("RoleClaims","Identity");
           
        builder.HasOne(u => u.Role).WithMany(u => u.Claims).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);


    }
}
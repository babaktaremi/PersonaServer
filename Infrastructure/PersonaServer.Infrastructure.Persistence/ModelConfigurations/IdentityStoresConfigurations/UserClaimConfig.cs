using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.IdentityStoresConfigurations;

internal class UserClaimConfig:IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {

        builder.HasOne(u => u.User).WithMany(u => u.Claims).HasForeignKey(u => u.UserId);
        builder.ToTable("UserClaims","Identity");
    }
}
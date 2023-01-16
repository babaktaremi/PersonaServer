using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.IdentityStoresConfigurations;

internal class UserTokenConfig:IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {

        builder.HasOne(u => u.User).WithMany(u => u.Tokens).HasForeignKey(u => u.UserId);
        builder.ToTable("UserTokens","Identity");
    }
}
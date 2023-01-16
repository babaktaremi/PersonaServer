using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.SsoStoresConfigurations;

internal class TokenStoreConfig:IEntityTypeConfiguration<TokenStore>
{
    public void Configure(EntityTypeBuilder<TokenStore> builder)
    {
        builder.ToTable("TokenStores", "Sso");
    }
}
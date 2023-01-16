using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.SsoStoresConfigurations;

internal class AuthorizationStoreConfig:IEntityTypeConfiguration<AuthorizationStore>
{
    public void Configure(EntityTypeBuilder<AuthorizationStore> builder)
    {
        builder.ToTable("AuthorizationStores", "Sso");
    }
}
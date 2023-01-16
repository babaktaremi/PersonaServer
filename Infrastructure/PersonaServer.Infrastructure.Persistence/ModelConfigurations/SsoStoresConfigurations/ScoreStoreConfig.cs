using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.SsoStoresConfigurations;

internal class ScoreStoreConfig:IEntityTypeConfiguration<ScopeStore>
{
    public void Configure(EntityTypeBuilder<ScopeStore> builder)
    {
        builder.ToTable("ScopeStores", "Sso");
    }
}
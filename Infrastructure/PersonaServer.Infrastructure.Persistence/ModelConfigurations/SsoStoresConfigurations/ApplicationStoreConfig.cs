using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.SsoStoresConfigurations;

internal class ApplicationStoreConfig:IEntityTypeConfiguration<ApplicationStore>
{
    public void Configure(EntityTypeBuilder<ApplicationStore> builder)
    {
        builder.ToTable("ApplicationStores", "Sso");
    }
}
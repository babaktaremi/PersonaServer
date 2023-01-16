using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.IdentityStoresConfigurations;

internal class RoleConfig:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles","Identity");
    }
}
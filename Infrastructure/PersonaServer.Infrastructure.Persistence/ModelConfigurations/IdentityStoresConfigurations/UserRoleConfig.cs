using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Persistence.ModelConfigurations.IdentityStoresConfigurations;

internal class UserRoleConfig:IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {

        builder.HasOne(u => u.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId);
        builder.HasOne(u => u.Role).WithMany(u => u.Users).HasForeignKey(u => u.RoleId);
        builder.ToTable("UserRoles","Identity");
    }
}
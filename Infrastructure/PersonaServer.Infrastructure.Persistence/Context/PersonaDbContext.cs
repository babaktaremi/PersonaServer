using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonaServer.Infrastructure.Persistence.Extensions;
using PersonaServer.Stores.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Infrastructure.Persistence.Context;

public class PersonaDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public PersonaDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IStoreEntity).Assembly;
        modelBuilder.RegisterAllEntities<IStoreEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonaDbContext).Assembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddPluralizingTableNameConvention();


    }

}
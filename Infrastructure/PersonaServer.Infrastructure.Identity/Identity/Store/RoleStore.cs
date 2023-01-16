using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PersonaServer.Infrastructure.Persistence.Context;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Identity.Identity.Store;

public class RoleStore:RoleStore<Role,PersonaDbContext,Guid,UserRole,RoleClaim>
{
    public RoleStore(PersonaDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}
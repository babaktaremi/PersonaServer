using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PersonaServer.Infrastructure.Persistence.Context;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Identity.Identity.Store;

public class AppUserStore:UserStore<User,Role,PersonaDbContext,Guid,UserClaim,UserRole,UserLogin,UserToken,RoleClaim>
{
    public AppUserStore(PersonaDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
    {
    }
}
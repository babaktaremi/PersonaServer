using Microsoft.AspNetCore.Identity;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Identity.Identity.validator;

public class AppRoleValidator:RoleValidator<Role>
{
    public AppRoleValidator(IdentityErrorDescriber errors):base(errors)
    {
            
    }

    public override Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
    {
        return base.ValidateAsync(manager, role);
    }
}
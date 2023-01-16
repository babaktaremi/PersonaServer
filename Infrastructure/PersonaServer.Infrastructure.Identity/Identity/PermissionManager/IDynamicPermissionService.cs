using System.Security.Claims;

namespace PersonaServer.Infrastructure.Identity.Identity.PermissionManager;

public interface IDynamicPermissionService
{
    bool CanAccess(ClaimsPrincipal user, string area, string controller, string action);
}
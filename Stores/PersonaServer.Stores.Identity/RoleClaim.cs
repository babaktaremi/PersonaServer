using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class RoleClaim:IdentityRoleClaim<Guid>,IStoreEntity
{
    public RoleClaim()
    {
        CreatedClaim=DateTime.Now;
    }

    public DateTime CreatedClaim { get; set; }
    public Role Role { get; set; }

}
using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class UserClaim:IdentityUserClaim<Guid>,IStoreEntity
{
    public User User { get; set; }
}
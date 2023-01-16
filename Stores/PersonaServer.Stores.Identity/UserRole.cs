using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class UserRole : IdentityUserRole<Guid>,IStoreEntity
{
    public User User { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedUserRoleDate { get; set; }

}
using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class User:IdentityUser<Guid>,IStoreEntity
{

    public string Name { get; set; }
    public string FamilyName { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserLogin> Logins { get; set; }
    public ICollection<UserClaim> Claims { get; set; }
    public ICollection<UserToken> Tokens { get; set; }

    #region Navigation Properties


    #endregion

}
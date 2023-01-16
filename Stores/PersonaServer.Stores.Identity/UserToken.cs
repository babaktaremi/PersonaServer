using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class UserToken:IdentityUserToken<Guid>,IStoreEntity
{
    public UserToken()
    {
        GeneratedTime=DateTime.Now;
    }

    public User User { get; set; }
    public DateTime GeneratedTime { get; set; }

}
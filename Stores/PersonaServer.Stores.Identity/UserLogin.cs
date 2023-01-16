using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class UserLogin:IdentityUserLogin<Guid>,IStoreEntity
{
    public UserLogin()
    {
        LoggedOn=DateTime.Now;
    }

    public User User { get; set; }
    public DateTime LoggedOn { get; set; }
}
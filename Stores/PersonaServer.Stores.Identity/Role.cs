using Microsoft.AspNetCore.Identity;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Identity;

public class Role:IdentityRole<Guid>,IStoreEntity
{
    public Role()
    {
        CreatedDate=DateTime.Now;
    }

    public string DisplayName { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<RoleClaim> Claims { get; set; }
    public ICollection<UserRole> Users { get; set; }


}
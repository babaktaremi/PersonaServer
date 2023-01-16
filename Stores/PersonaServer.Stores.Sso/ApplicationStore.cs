using OpenIddict.EntityFrameworkCore.Models;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Sso;

public class ApplicationStore : OpenIddictEntityFrameworkCoreApplication<Guid, AuthorizationStore, TokenStore>, IStoreEntity
{
}
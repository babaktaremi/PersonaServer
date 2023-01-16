using OpenIddict.EntityFrameworkCore.Models;
using PersonServer.Stores.Shared.Interfaces;

namespace PersonaServer.Stores.Sso;

public class AuthorizationStore: OpenIddictEntityFrameworkCoreAuthorization<Guid, ApplicationStore, TokenStore>, IStoreEntity
{
}
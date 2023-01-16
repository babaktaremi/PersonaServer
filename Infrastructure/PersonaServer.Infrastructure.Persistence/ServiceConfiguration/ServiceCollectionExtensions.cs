using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonaServer.Infrastructure.Persistence.Context;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddDbContext<PersonaDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
            
            options.UseOpenIddict<ApplicationStore, AuthorizationStore, ScopeStore, TokenStore, Guid>();

        });

        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using PersonaServer.Infrastructure.OpenIdDict.Workers;
using PersonaServer.Infrastructure.Persistence.Context;
using PersonaServer.Stores.Sso;

namespace PersonaServer.Infrastructure.OpenIdDict.Configurations;

public static class OpenIdDictConfiguration
{
    public static IServiceCollection AddOpenIdDict(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.SetDefaultAuthorizationEntity<AuthorizationStore>();
                options.SetDefaultApplicationEntity<ApplicationStore>();
                options.SetDefaultScopeEntity<ScopeStore>();
                options.SetDefaultTokenEntity<TokenStore>();

                options.UseEntityFrameworkCore(builder =>
                {
                    builder.UseDbContext<PersonaDbContext>();
                    builder
                        .ReplaceDefaultEntities<ApplicationStore, AuthorizationStore, ScopeStore, TokenStore, Guid>();
                });

            })
            .AddServer(options =>
            {
                options
                    .AllowClientCredentialsFlow()
                    .AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow();

                options.SetAuthorizationEndpointUris("connect/authorize")
                    .SetLogoutEndpointUris("connect/logout")
                    .SetTokenEndpointUris("connect/token")
                    .SetUserinfoEndpointUris("connect/userinfo");

                // Encryption and signing of tokens
                options
                    .AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey();

                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));

                // Register scopes (permissions)
                options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile, OpenIddictConstants.Scopes.Roles, OpenIddictConstants.Scopes.Phone);


                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough()
                    .EnableVerificationEndpointPassthrough();

            }).AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                 options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();

                options.UseSystemNetHttp();
            });

        services.AddHostedService<AddTestDataWorker>();

        return services;
    }
}
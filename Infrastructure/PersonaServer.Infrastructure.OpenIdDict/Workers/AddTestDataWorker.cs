﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;

namespace PersonaServer.Infrastructure.OpenIdDict.Workers;

internal class AddTestDataWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AddTestDataWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();


        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("main-Api", stoppingToken) is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "main-Api",
                ConsentType = OpenIddictConstants.ConsentTypes.Explicit,
                ClientSecret = "main-Api-Secret",
                DisplayName = "Interaction With Main API",
                Type = OpenIddictConstants.ClientTypes.Confidential,
                RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback"), new Uri("https://localhost:5552"),new Uri("https://oidcdebugger.com/debug") , new Uri("https://localhost:7098/signin-oidc") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Logout,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.GrantTypes.Implicit,

                    OpenIddictConstants.Permissions.ResponseTypes.IdToken,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,

                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange,
                },
                PostLogoutRedirectUris = { new Uri("https://localhost:7098/signout-callback-oidc") }
            }, stoppingToken);
        }
    }
}
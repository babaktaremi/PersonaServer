using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PersonaServer.Infrastructure.Identity.Identity;
using PersonaServer.Infrastructure.Identity.Identity.Extensions;
using PersonaServer.Infrastructure.Identity.Identity.Manager;
using PersonaServer.Infrastructure.Identity.Identity.PermissionManager;
using PersonaServer.Infrastructure.Identity.Identity.SeedDatabaseService;
using PersonaServer.Infrastructure.Identity.Identity.Store;
using PersonaServer.Infrastructure.Identity.Identity.validator;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Infrastructure.Identity.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterIdentityServices(this IServiceCollection services)
    {
      
        services.AddScoped<ISeedDataBase, SeedDataBase>();

        services.AddScoped<IUserValidator<User>, AppUserValidator>();
        services.AddScoped<UserValidator<User>, AppUserValidator>();

        services.AddScoped<IUserClaimsPrincipalFactory<User>, AppUserClaimsPrincipleFactory>();

        services.AddScoped<IRoleValidator<Role>, AppRoleValidator>();
        services.AddScoped<RoleValidator<Role>, AppRoleValidator>();

        services.AddScoped<IAuthorizationHandler, DynamicPermissionHandler>();
        services.AddScoped<IDynamicPermissionService, DynamicPermissionService>();
        services.AddScoped<IRoleStore<Role>, RoleStore>();
        services.AddScoped<IUserStore<User>, AppUserStore>();

        services.AddIdentity<User, Role>(options =>
            {
                options.Stores.ProtectPersonalData = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                options.User.RequireUniqueEmail = true;
                //options.Stores.ProtectPersonalData = true;


            }).AddUserStore<AppUserStore>()
            .AddRoleStore<RoleStore>().
            //.AddUserValidator<AppUserValidator>().
            //AddRoleValidator<AppRoleValidator>().
            AddUserManager<AppUserManager>().
            AddRoleManager<AppRoleManager>().
            AddErrorDescriber<AppErrorDescriber>()
            //.AddClaimsPrincipalFactory<AppUserClaimsPrincipleFactory>()
            .AddDefaultTokenProviders().
            AddSignInManager<AppSignInManager>()
            .AddDefaultTokenProviders()
            .AddPasswordlessLoginTotpTokenProvider();


        //For [ProtectPersonalData] Attribute In Identity

        //services.AddScoped<ILookupProtectorKeyRing, KeyRing>();

        //services.AddScoped<ILookupProtector, LookupProtector>();

        //services.AddScoped<IPersonalDataProtector, PersonalDataProtector>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(ConstantPolicies.DynamicPermission, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new DynamicPermissionRequirement());
            });
        });


        return services;
    }
}
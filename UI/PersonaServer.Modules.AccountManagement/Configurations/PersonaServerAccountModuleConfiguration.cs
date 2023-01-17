using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace PersonaServer.Modules.AccountManagement.Configurations;

public static class PersonaServerAccountModuleConfiguration
{
    public static IServiceCollection AddAccountModuleServices(this IServiceCollection services)
    {
        services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
        {
            options.FileProviders.Add(new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory)!, "PersonaServer.Modules.AccountManagement")));
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
            });


        return services;
    }

    public static WebApplication UseAccountModule(this WebApplication app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory)!, "PersonaServer.Modules.AccountManagement", "wwwroot")),
            RequestPath = "/Account/StaticFiles"
        });

        #region Add Test Client

        

        #endregion

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
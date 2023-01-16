using PersonaServer.Infrastructure.Identity.ServiceConfiguration;
using PersonaServer.Infrastructure.OpenIdDict.Configurations;
using PersonaServer.Infrastructure.Persistence.ServiceConfiguration;
using PersonaServer.Modules.AccountManagement.Configurations;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
       .RegisterIdentityServices()
       .AddOpenIdDict()
       .AddPersistenceServices(configuration);


builder.Services.AddAccountModuleServices();


var app = builder.Build();

app.MapGet("/", () => "For Documents refer to the github page: https://...");

app.UseAccountModule();

app.Run();

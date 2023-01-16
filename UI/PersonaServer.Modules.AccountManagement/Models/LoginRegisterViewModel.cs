namespace PersonaServer.Modules.AccountManagement.Models;

public record LoginRegisterViewModel(LoginViewModel LoginViewModel = default,
    RegisterViewModel RegisterViewModel = default);
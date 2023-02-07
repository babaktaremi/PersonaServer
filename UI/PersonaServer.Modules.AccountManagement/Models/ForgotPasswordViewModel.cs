using System.ComponentModel.DataAnnotations;

namespace PersonaServer.Modules.AccountManagement.Models;

public record ForgotPasswordViewModel([Required(ErrorMessage = "Please enter username or password")] string UserNameOrEmail);
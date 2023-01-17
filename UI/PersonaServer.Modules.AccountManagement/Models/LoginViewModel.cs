using System.ComponentModel.DataAnnotations;

namespace PersonaServer.Modules.AccountManagement.Models;

public record LoginViewModel(
    [ Required(ErrorMessage = "Please Enter Your Email"),EmailAddress(ErrorMessage = "Please Enter Your Valid Email")]
    string Email,
    [Required(ErrorMessage = "Please Enter Your Password")]
    string Password,
    string ReturnUrl,
    bool RememberMe=false);
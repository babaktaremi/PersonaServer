using System.ComponentModel.DataAnnotations;

namespace PersonaServer.Modules.AccountManagement.Models;

public record RegisterViewModel(
    [Required(ErrorMessage = "Please Enter Your User Name")]
    string UserName,
    [EmailAddress(ErrorMessage = "Please Enter Valid Your Email")]
    string Email,
    [Required(ErrorMessage = "Please Enter Your Password")]
    string Password,
    [Required(ErrorMessage = "Please Enter Your Name")]
    string Name,
    [Required(ErrorMessage = "Please Enter Your Family Name")]
    string FamilyName,
    string ReturnUrl);
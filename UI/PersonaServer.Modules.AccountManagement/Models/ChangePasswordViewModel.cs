using System.ComponentModel.DataAnnotations;

namespace PersonaServer.Modules.AccountManagement.Models;

public record ChangePasswordViewModel(Guid UserId,
    string ChangePasswordToken
)
{
    [Required(ErrorMessage = "Please enter your new password")]
    public string NewPassword { get; init; }

    [Compare(nameof(NewPassword),ErrorMessage = "New password and repeat password is not same")]
    public string RepeatPassword { get; init; }
}
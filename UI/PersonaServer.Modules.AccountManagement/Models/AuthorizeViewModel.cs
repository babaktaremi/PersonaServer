using System.ComponentModel.DataAnnotations;

namespace PersonaServer.Modules.AccountManagement.Models;

public class AuthorizeViewModel
{
    public AuthorizeViewModel()
    {
        SelectedScopes = new();
    }

    [Display(Name = "Application")]
    public string ApplicationName { get; set; }

    [Display(Name = "Scope")]
    public string Scope { get; set; }

    public List<string> SelectedScopes { get; set; }
}
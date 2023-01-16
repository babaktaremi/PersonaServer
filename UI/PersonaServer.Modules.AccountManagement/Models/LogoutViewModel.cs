using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonaServer.Modules.AccountManagement.Models;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}
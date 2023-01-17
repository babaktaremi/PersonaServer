using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaServer.Modules.AccountManagement.Models
{
    public record EmailConfirmationViewModel(bool IsSuccess, string Message);
}

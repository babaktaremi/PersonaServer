using Microsoft.AspNetCore.Identity;

namespace PersonaServer.Infrastructure.Identity.Identity;

public class AppErrorDescriber:IdentityErrorDescriber
{
    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = "DefaultError",
            Description = "There was a problem. Try again"
        };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = "This email already exists. Login or use another email"
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = "this username already exists. Login or use another email"
        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = "Password is not correct. Please try again"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = "Password is too short. Try another password"
        };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = "Username not valid. Try another username"
        };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = "Email is not valid. Please use a valid email"
        };
    }

    public override IdentityError InvalidToken()
    {
            
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = "Code is incorrect. Please try again"
        };
    }


}
using Microsoft.AspNetCore.Mvc;
using PersonaServer.Infrastructure.Identity.Identity.Manager;
using PersonaServer.Modules.AccountManagement.Models;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Modules.AccountManagement.AccountControllers;

[Route("/Account/[action]")]
[AutoValidateAntiforgeryToken]
public class AccountController : Controller
{
    private readonly AppUserManager _userManager;
    private readonly AppSignInManager _signInManager;

    public AccountController(AppUserManager userManager, AppSignInManager signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl="")
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Content("Bad Return URL Format");
        }

        var loginModel = new LoginRegisterViewModel(new LoginViewModel(string.Empty, string.Empty, returnUrl, false));

        return View(loginModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRegisterViewModel model)
    {
        if (!ModelState.IsValid && model.RegisterViewModel is null)
            return View(model);


        var user = await _userManager.FindByEmailAsync(model.LoginViewModel.Email);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User Not Found. Please Register");
            return View(model);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, model.LoginViewModel.Password, model.LoginViewModel.RememberMe, true);

        if (signInResult.Succeeded)
            return Redirect(model.LoginViewModel.ReturnUrl);

        ModelState.AddModelError(string.Empty, "Your Account Is Disabled. Contact Administrator");
        return View(model);
    }

    public IActionResult Register([FromQuery(Name = "ReturnUrl")] string returnUrl = "")
    {
        var model = new LoginRegisterViewModel(RegisterViewModel: new RegisterViewModel(string.Empty, string.Empty,
            string.Empty, string.Empty, string.Empty, returnUrl));

        return View("Login",model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(LoginRegisterViewModel model)
    {
        if (!ModelState.IsValid && model.LoginViewModel is null)
            return View("Login", model);

        var checkUser = await _userManager.FindByEmailAsync(model.RegisterViewModel.Email);

        if (checkUser is not null)
        {
            ModelState.AddModelError(string.Empty, "User Already Exists. Please Login");
            return View("Login", model);
        }

        var user = new User()
        {
            Email = model.RegisterViewModel.Email,
            UserName = model.RegisterViewModel.UserName,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            Name = model.RegisterViewModel.Name,
            FamilyName = model.RegisterViewModel.FamilyName
        };

        var newUser = await _userManager.CreateAsync(user, model.RegisterViewModel.Password);

        if (newUser.Succeeded)
        {
            var signInResult =
                await _signInManager.PasswordSignInAsync(user, model.RegisterViewModel.Password, false, true);

            if (signInResult.Succeeded)
                return Redirect(model.RegisterViewModel.ReturnUrl??"/");

            ModelState.AddModelError(string.Empty, "Your Account Is Disabled. Contact Administrator");
            return View("Login", model);
        }

        ModelState.AddModelError(string.Empty, string.Join("\n", newUser.Errors.Select(c => c.Description)));

        return View("Login", model);
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonaServer.Infrastructure.Identity.Identity.Manager;
using PersonaServer.Modules.AccountManagement.Helpers;
using PersonaServer.Modules.AccountManagement.Models;
using PersonaServer.Stores.Identity;

namespace PersonaServer.Modules.AccountManagement.AccountControllers;

[Route("/Account/[action]")]
[AutoValidateAntiforgeryToken]
public class AccountController : Controller
{
    private readonly AppUserManager _userManager;
    private readonly AppSignInManager _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AppUserManager userManager, AppSignInManager signInManager, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public IActionResult Login([FromQuery(Name = "ReturnUrl")] string returnUrl="")
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
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

        if (!await _userManager.CheckPasswordAsync(user, model.LoginViewModel.Password))
        {
            ModelState.AddModelError(string.Empty, "Password is not correct");
            return View(model);
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = Url.Action("ConfirmEmail", new { userId = user.Id, confirmToken = emailConfirmationToken });

            _logger.LogWarning("Email Confirmation Url is {0}",url);

            return RedirectToAction("ConfirmEmailRequired");
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, model.LoginViewModel.Password, model.LoginViewModel.RememberMe, true);

        if (signInResult.Succeeded)
            return Redirect(model.LoginViewModel.ReturnUrl);

        ModelState.AddModelError(string.Empty, signInResult.ToString());
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

        //var checkUser = await _userManager.FindByEmailAsync(model.RegisterViewModel.Email);

        //if (checkUser is not null)
        //{
        //    ModelState.AddModelError(string.Empty, "User Already Exists. Please Login");
        //    return View("Login", model);
        //}

        var user = new User()
        {
            Email = model.RegisterViewModel.Email,
            UserName = model.RegisterViewModel.UserName,
            Name = model.RegisterViewModel.Name,
            FamilyName = model.RegisterViewModel.FamilyName
        };

        var newUser = await _userManager.CreateAsync(user, model.RegisterViewModel.Password);

        if (!newUser.Succeeded)
        {
            foreach (var identityError in newUser.Errors)
            {
                ModelState.AddModelError(string.Empty,identityError.Description);
            }

            return View("Login",model);
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = Url.Action("ConfirmEmail", new { userId = user.Id, confirmToken = emailConfirmationToken });

                _logger.LogWarning("Email Confirmation Url is {0}", url);

            return RedirectToAction("ConfirmEmailRequired");
        }

        ModelState.AddModelError(string.Empty, string.Join("\n", newUser.Errors.Select(c => c.Description)));

        return View("Login", model);
    }

    public IActionResult ConfirmEmailRequired()
    {
        return View();
    }

    public async Task<IActionResult> ConfirmEmail(Guid userId, string confirmToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return View(new EmailConfirmationViewModel(false, "User Not Found"));

        if (user.EmailConfirmed)
            return View(new EmailConfirmationViewModel(false, "This Email is Already Confirmed"));

        var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, confirmToken);

        if (confirmEmailResult.Succeeded)
            return View(new EmailConfirmationViewModel(true, "Your Email Confirmed Successfully"));

        return View(new EmailConfirmationViewModel(false,
            string.Join("\n", confirmEmailResult.Errors.Select(c => c.Description))));
    }

    public IActionResult ForgotPassword() => View();

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new EmailAddressAttribute().IsValid(model.UserNameOrEmail)
            ? await _userManager.FindByEmailAsync(model.UserNameOrEmail): await _userManager.FindByNameAsync(model.UserNameOrEmail);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty,"User not found");
            return View(model);
        }

        var forgotPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var url = Url.Action("ResetPassword", new { userId = user.Id, forgotPasswordToken = forgotPasswordToken });

        _logger.LogWarning("Email Confirmation Url is {0}", url);

        return View("_MessageView",
            new ShowMessageViewModel(ShowMessageViewModel.MessageType.Success,
                $"A link to reset your password sent to email {user.Email.MaskEmail()}"));
    }

    public async Task<IActionResult> ResetPassword(Guid userId, string forgotPasswordToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return NotFound();

        var changePasswordVerificationToken = await _userManager.VerifyUserTokenAsync(user,
            _userManager.Options.Tokens.ChangeEmailTokenProvider, "ResetPassword", forgotPasswordToken);


        if (!changePasswordVerificationToken)
            return NotFound();


        return View(new ChangePasswordViewModel(user.Id, forgotPasswordToken));
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ChangePasswordViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());

        if (user == null)
            return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        var changePasswordResult =
            await _userManager.ResetPasswordAsync(user, model.ChangePasswordToken, model.NewPassword);

        if (changePasswordResult.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);

            return View("_MessageView",
                new ShowMessageViewModel(ShowMessageViewModel.MessageType.Success,
                    "Your password changed successfully. you can now login"));
        }

        foreach (var identityError in changePasswordResult.Errors)
        {
            ModelState.AddModelError(string.Empty,identityError.Description);
        }

        return View(model);
    }
}
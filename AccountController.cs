using Microsoft.AspNetCore.Mvc;
using SRS_Hotels.Web.Areas.Auth.ViewModels;
using SRS_Hotels.Web.Services;

namespace SRS_Hotels.Web.Areas.Auth.Controllers;

[Area("Auth")]
public class AccountController : Controller
{
    private readonly AuthService _auth;

    public AccountController(AuthService auth)
    {
        _auth = auth;
    }

    // LOGIN
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _auth.LoginAsync(model);

        if (result is null || !result.Successful)
        {
            ModelState.AddModelError("", result?.Message ?? "Error");
            return View(model);
        }

        return RedirectToAction("Index", "Home", new { area = "" });
    }

    // SIGN UP
    [HttpGet]
    public IActionResult SignUp()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _auth.SignUpAsync(model);

        if (result is null || !result.Successful)
        {
            ModelState.AddModelError("", result?.Message ?? "Error");
            return View(model);
        }

        return RedirectToAction("Login");
    }
}
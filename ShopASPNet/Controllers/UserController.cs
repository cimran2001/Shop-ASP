using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopASPNet.Models.Requests;
using ShopASPNet.Models.UserModels;

namespace ShopASPNet.Controllers; 

public class UserController : Controller {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    
    public UserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult LogIn(string? returnUrl = null) {
        TempData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(UserLogInRequest request) {
        var result = await _signInManager.PasswordSignInAsync(
            request.Username, request.Password, request.RememberMe, lockoutOnFailure: true);

        if (!result.Succeeded) {
            TempData["Error"] = "Something went wrong";
            return View();
        }

        if (TempData["ReturnUrl"] is string returnUrl)
            return LocalRedirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult SignUp() {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUp(UserSignUpRequest request) {
        if (!ModelState.IsValid) {
            TempData["Error"] = "Something went wrong.";
            return View();
        }

        var user = new AppUser {
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) {
            TempData["Error"] = "Something went wrong";
            return View();
        }

        var logInRequest = new UserLogInRequest {
            Username = user.UserName,
            Password = request.Password,
        };
        return await LogIn(logInRequest);
    }

    public async Task<IActionResult> LogOut() {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
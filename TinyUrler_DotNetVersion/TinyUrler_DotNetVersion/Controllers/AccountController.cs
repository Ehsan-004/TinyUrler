using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyUrler_DotNetVersion.Data;
using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;
using TinyUrler_DotNetVersion.ViewModels;

namespace TinyUrler_DotNetVersion.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly TContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public IActionResult All()
    {
        var users = _context.Users.ToList();
        return Json(new { users = users });
    }

    public IActionResult Login()
    {
        var response = new LoginViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByNameAsync(loginViewModel.Username);

        if (user != null)
        {
            // User is found
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

            if (passwordCheck)
            {
                // Password correct, Sign In.=
                 var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                 if (result.Succeeded)
                 {
                     return RedirectToAction("Index", "Home");
                 }
            }
            // Password incorrect
            TempData["Error"] = "Invalid username or password";
            return View(loginViewModel);
        }
        // User not found
        TempData["Error"] = "Invalid username or password";
        return View(loginViewModel);
    }
    
    public IActionResult Register()
    {
        var response = new RegisterViewModel();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);
        
        var user = await _userManager.FindByNameAsync(registerViewModel.Username);
        if (user != null)
        {
            TempData["Error"] = "Username is already taken";
            return View(registerViewModel);
        }

        var newUser = new AppUser
        {
            UserName = registerViewModel.Username,
            Email = registerViewModel.EmailAddress,
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (newUserResponse.Succeeded)
        {
            // await _userManager.AddToRoleAsync(newUser, "User");
            // var result = await _signInManager.PasswordSignInAsync(newUser, registerViewModel.Password, false, false);
            // if (result.Succeeded)
            // {
            //     return RedirectToAction("Index", "Home");
            // }    
            return RedirectToAction("Login");
        }
        else
        {
            TempData["error"] = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one non-alphanumeric character.";
            return View(registerViewModel);
        }
        
        TempData["Error"] = "Something went wrong";
        
        return View(registerViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "home");
    }
}
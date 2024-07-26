//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using System.Threading.Tasks;
//using AuthenticationSystem.Models; // Replace with your application's namespace
//using Microsoft.AspNetCore.Authorization;

//public class AccountController : Controller
//{
//    private readonly SignInManager<IdentityUser> _signInManager;

//    public AccountController(SignInManager<IdentityUser> signInManager)
//    {
//        _signInManager = signInManager;
//    }


//    [AllowAnonymous]
//    [HttpGet]
//    public IActionResult Login(string returnUrl = null)
//    {
//        return View();
//    }

//    [AllowAnonymous]
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
//    {
//        if (ModelState.IsValid)
//        {
//            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

//            if (result.Succeeded)
//            {
//                // Check if the user is in the Admin role
//                if (User.IsInRole("Admin"))
//                {
//                    return RedirectToAction("Index", "Admin"); // Redirect to the Admin panel
//                }
//                else
//                {
//                    return RedirectToLocal(returnUrl); // Redirect to the default location
//                }
//            }
//            // Handle login failure
//        }
//        // Handle invalid login data

//        return View(model); // Return to the login view
//    }

//}



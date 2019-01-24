using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreVideo.ViewModels;
using AspNetCoreVideo.Entities;
using Microsoft.AspNetCore.Identity;


namespace AspNetCoreVideo.Controllers
{
    public class AccountController : Controller
    {

        // Injecting signInManager and userManager.

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // When user first clicks the Register link, simply display the page.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // When user finishes registering, it shows this version of the page, or the POST version.
            if (!ModelState.IsValid) return View();
            var user = new User { UserName = model.Username }; // creating an instance of type User
            var result = await _userManager.CreateAsync(user, model.Password); // use the UserManager to create a new account
            if (result.Succeeded)
            {
                // if the new user is created successfully, sign the user in and return to Home.
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If there was something wrong with the registration, display the errors.
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        // returnUrl saves the current URL from when you tried to reach a page that requires you to be logged in, while you weren't logged in.

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false); // Last parameter states if user should be locked out if they provide wrong credentials.
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) // checks that URL isn't null or empty, and that it is a local URL
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Login failed");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

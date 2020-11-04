using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photographie.Models;
using Photographie.ViewModel;

namespace Photographie.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signIn)
        {
            UserManager = userManager;
            SignIn = signIn;
        }

        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await SignIn.PasswordSignInAsync(login.Email, login.password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "MyProtfolio");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await SignIn.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
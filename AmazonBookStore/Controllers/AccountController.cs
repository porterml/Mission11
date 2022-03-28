using AmazonBookStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonBookStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signIn;

        public AccountController(UserManager<IdentityUser> um, SignInManager<IdentityUser> sim)
        {
            userManager = um;
            signIn = sim;
        }

        [HttpGet]
        public IActionResult Login(string URL)
        {
            return View(new LoginModel { returnURL = URL });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(login.Username);

                if (user != null)
                {
                    await signIn.SignOutAsync();

                    if ((await signIn.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                    {
                        return Redirect(login?.returnURL ?? "/Admin");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Username or Password");
            return View(login);
        }


        public async Task<RedirectResult> Logout (string returnURL = "/")
        {
            await signIn.SignOutAsync();

            return Redirect(returnURL);
        }
    }
}

﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loanda.Web.Models;
using Microsoft.AspNetCore.Identity;
using Loanda.Entities;

namespace Loanda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        //private readonly IGmailApi gmailApi;

        public HomeController(SignInManager<User> signInManager, UserManager<User> userManager/*, IGmailApi gmailApi*/)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            //this.gmailApi = gmailApi;
        }

        public /*async Task <*/IActionResult/*>*/ Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = userManager.GetUserAsync(User);
                if (user.Result.IsFirstLogin)
                {
                    return View("ChangePassword");
                }
                //await this.gmailApi.GetEmailsFromGmailAsync();
                return View("Privacy");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            var user = userManager.GetUserAsync(User);
            if (user.Result.IsFirstLogin)
            {
                return View("ChangePassword");
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public async Task<IActionResult> Login(LoginViewModel input)
        //{
        //    var result = await signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
        //    if (result.Succeeded)
        //    {
        //        var user = userManager.FindByNameAsync(input.Email);
        //        return Redirect("Privacy");
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<bool> CheckForCredentials(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public async Task<bool> CheckIfFirstLogin()
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                return user.IsFirstLogin;
            }

            return false;
        }
    }
}

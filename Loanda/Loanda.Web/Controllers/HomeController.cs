using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Loanda.Web.Models;
using Loanda.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;
using Loanda.Entities;
using Loanda.EmailClient.Contracts;

namespace Loanda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IGmailApi gmailApi;

        public HomeController(SignInManager<User> signInManager, UserManager<User> userManager, IGmailApi gmailApi)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.gmailApi = gmailApi;
        }

        public async Task <IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                //await this.gmailApi.GetEmailsFromGmail();
                return View("Privacy");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            var result = await signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = userManager.FindByNameAsync(input.Email);
                return Redirect("Privacy");
            }
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loanda.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly LoandaContext context;

        public RegisterController(IUserService userService, UserManager<User> userManager, LoandaContext context)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel vm)
        {
            if (await userService.CheckForEmail(vm.Email))
            {
                return new JsonResult("true email");
            }
            if (await userService.CheckForUserName(vm.Username))
            {
                return new JsonResult("true username");
            }
            var newUser = new User
            {
                UserName = vm.Username,
                Email = vm.Email,
                IsFirstLogin = true
            };
            var result = await userManager.CreateAsync(newUser, vm.Password);
            await userManager.AddToRoleAsync(newUser, "USER");
            return new JsonResult("false");
        }

        [HttpPost]
        public bool Changepassword(string newPass, string confirmPass)
        {
            if (newPass.Length > 4 && newPass.Equals(confirmPass))
            {
                var user = userManager.GetUserAsync(User);
                var oldPassword = user.Result.PasswordHash;
                userManager.ChangePasswordAsync(user.Result, oldPassword, newPass);

                user.Result.IsFirstLogin = false;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

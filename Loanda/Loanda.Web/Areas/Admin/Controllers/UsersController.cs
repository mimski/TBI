using Loanda.Entities;
using Loanda.Web.Areas.Admin.Models;
using Loanda.Web.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Loanda.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserManager<User> _userManager;
        private readonly int PAGE_SIZE = 20;

        [TempData]
        public string StatusMessage { get; set; }

        public UsersController(IUserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel(_userManager.Users, 1, PAGE_SIZE);
            indexViewModel.StatusMessage = StatusMessage;

            return View(indexViewModel);
        }

        public IActionResult UserGrid(int? page)
        {
            var pagedUsers = _userManager.Users
                                         .Select(u => new UserViewModel(u))
                                         .ToPagedList(page ?? 1, PAGE_SIZE);

            return PartialView("_UserGrid", pagedUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserSettings(UserModalModelView input)
        {
            if (input.IsAdmin) await this.AsignAdminRole(input.ID);
            else await this.RemoveAdminRole(input.ID);

            if (input.IsLocked) await this.LockUser(input.ID);
            else await this.UnlockUser(input.ID);

            if (!string.IsNullOrEmpty(input.ConfirmPassword))
            {
                await this.ChangePassword(input);
            }

            return this.RedirectToAction(nameof(Index));
        }

        public async Task LockUser(string userID)
        {
            var user = _userManager.Users.Where(u => u.Id == userID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return;
            }

            var enableLockOutResult = await _userManager.SetLockoutEnabledAsync(user, true);
            if (!enableLockOutResult.Succeeded)
            {
                this.StatusMessage = "Error: Could enable the lockout on the user!";
                return;
            }
            var lockoutTimeResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));
            if (!lockoutTimeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not add time to user's lockout!";
                return;
            }
            this.StatusMessage = "The user has been successfully locked for 10 years!";
        }

        public async Task UnlockUser(string userID)
        {
            var user = _userManager.Users.Where(u => u.Id == userID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return;
            }

            var lockoutTimeResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
            if (!lockoutTimeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not add time to user's lockout!";
                return;
            }

            this.StatusMessage = "The user has been successfully unlocked!";
        }

        public async Task ChangePassword(UserModalModelView input)
        {
            var user = _userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return;
            }

            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager.Instance, user, input.ConfirmPassword);
                if (!result.Succeeded)
                {
                    this.StatusMessage = $"Error: {string.Join(" ", result.Errors.Select(e => e.Description)).Replace(".", "!")}";
                    return;
                }
            }

            if (!ModelState.IsValid)
            {
                this.StatusMessage = "Error: Passwords do not match!";
                return;
            }

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not remove the old password!";
                return;
            }
            var addPasswordResult = await _userManager.AddPasswordAsync(user, input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not change the password!";
                return;
            }
            this.StatusMessage = "The user's password has been changed!";
        }

        public async Task AsignAdminRole(string userID)
        {
            User user = _userManager.Users.Where(u => u.Id == userID).FirstOrDefault();

            await _userManager.Instance.AddToRoleAsync(user, "Administrator");
        }

        public async Task RemoveAdminRole(string userID)
        {
            User user = _userManager.Users.Where(u => u.Id == userID).FirstOrDefault();

            await _userManager.Instance.RemoveFromRoleAsync(user, "Administrator");
        }
    }
}
using AdminDashboard.Models;
using AdminDashboard.Models.Users;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace AdminDashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
           _userManager = userManager;
           _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {

            var Users = await _userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Username = user.UserName!,
                Roles =  _userManager.GetRolesAsync(user).Result
            }).ToListAsync();

            return View(Users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id )
        {
            var User = await _userManager.FindByIdAsync(Id);

            var Roles = await _roleManager.Roles.ToListAsync();

            var UserModel = new UserRoleViewModel
            {
                UserId = User!.Id,
                Username = User.UserName!,
                Roles = Roles.Select(role => new UpdateRoleViewModel
                {

                    Id = role.Id,
                    Name = role.Name!,
                    IsSelected = _userManager.IsInRoleAsync(User, role.Name!).Result
                }).ToList()
            };


            return View(UserModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var User = await _userManager.FindByIdAsync(model.UserId);

            var Roles = await _userManager.GetRolesAsync(User);

            foreach (var role in model.Roles)
            {
                if(Roles.Any(r => r ==  role.Name ) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(User, role.Name);

                if (!Roles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(User, role.Name);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

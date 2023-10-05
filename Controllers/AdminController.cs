using AribTask.Seeds;
using AribTask.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            var result = _userManager.Users.Select(x => new UserDto { Username = x.UserName, Email = x.Email });
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(new UserDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            await DefaultUsers.SeedBasicUserAsync(_userManager, _roleManager, user.Username, user.Email, user.Password);
            return View(user);
        }
    }
}

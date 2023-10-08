using AribTask.Data;
using AribTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly ApplicationDbContext _context;
		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		this._context = context;
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Register()
		{

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> Register(RegisterViewModel RegisterViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = RegisterViewModel.Username, Email = RegisterViewModel.Email };
				var result = await userManager.CreateAsync(user, RegisterViewModel.Pwd);
				if (result.Succeeded)
				{
					await _context.Employees.AddAsync(new Models.Employee() {UserId=user.Id,Email=user.Email,UserName=user.UserName });
					await signInManager.SignInAsync(user, isPersistent: false);
					_context.SaveChanges();
					return RedirectToAction("Index", "EmployeeTask");

				}
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return View();
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Login()
		{

			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> login(LoginViewModel LoginViewModel)
		{
			if (ModelState.IsValid)
			{

				var result = await signInManager.PasswordSignInAsync(LoginViewModel.Email, LoginViewModel.Password, LoginViewModel.RememberMe, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "Invalid user name or password");

			}
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> LogOut(string returnUrl = null)
		{

			await signInManager.SignOutAsync();
			if (returnUrl != null)
			{
				return LocalRedirect(returnUrl);
			}
			return RedirectToAction("Login", "Account");
		}












		// GET: AccountController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: AccountController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AccountController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AccountController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AccountController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AccountController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AccountController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}

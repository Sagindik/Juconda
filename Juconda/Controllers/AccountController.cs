using Juconda.Core.Common;
using Juconda.Domain.Models;
using Juconda.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juconda.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_userManager.PasswordHasher = new CustomPasswordHasher();			
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult LogIn(string returnUrl = null)
		{
			return View(new LogInViewModel { ReturnUrl = returnUrl });
		}

        [HttpGet]
        public IActionResult PersonalCabinet(string returnUrl = null)
        {
            return View(new PersonalCabinetViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogIn(LogInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,
								model.RememberMe, lockoutOnFailure: true);

					if (result.Succeeded)
					{
						user.LastLoginDate = DateTime.UtcNow;
						await _userManager.UpdateAsync(user);						

						// проверяем, принадлежит ли URL приложению
						if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
							return Redirect(model.ReturnUrl);

						await _userManager.ResetAccessFailedCountAsync(user);

						return RedirectToAction("Index", "Home");
					}

					if (result.IsLockedOut)
					{
						var lockoutEnd = user.LockoutEnd != null && DateTime.Now.AddDays(1) > user.LockoutEnd.Value ?
							"Срок блокировки - " + user.LockoutEnd.Value.ToString("dd.MM.yyyy") : "";
						ModelState.AddModelError("IsBlocked", $"Пользователь заблокирован. {lockoutEnd}");
					}
					else
						ModelState.AddModelError("PasswordIsNotValid", "Неверный пароль");
				}
				else
				{
					ModelState.AddModelError("NotFound", $"Пользователь с логином '{model.UserName}' не найден");
				}
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = new User { Email = model.Email, UserName = model.UserName };
				// добавляем пользователя
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					// установка куки
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}
	}
}

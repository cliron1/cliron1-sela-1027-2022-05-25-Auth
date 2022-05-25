using CookieAuth.Models;
using CookieAuth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookieAuth.Controllers {
	public class AccountController : Controller {
		private IUserService _userService;

		public AccountController(IUserService userService) {
			_userService = userService;
		}

		[HttpGet("/login")]
		public IActionResult Login() {
			var model = new AuthenticateModel();
#if DEBUG
			model.Username = "test";
			model.Password = "1234";
#endif
			return View(model);
		}

		[HttpPost("/login")]
		public async Task<IActionResult> Login(AuthenticateModel model, string returnUrl) {
			var data = await _userService.Authenticate(model.Username, model.Password);

			if (data == null) {
				ModelState.AddModelError("", "Username or password is incorrect");
				return View();
			}

			var claims = new List<Claim> {
				new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
				new Claim(ClaimTypes.Name, data.FirstName)
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);
			await HttpContext.SignInAsync(principal,
				new AuthenticationProperties {
					ExpiresUtc = model.RememberMe
						? DateTime.UtcNow.AddDays(90)
						: DateTime.UtcNow.AddMinutes(20),
					IsPersistent = true,
					AllowRefresh = true,
					RedirectUri = "/login"
				});

			if (string.IsNullOrEmpty(returnUrl))
				returnUrl = "/";
			return LocalRedirect(returnUrl);
		}

		[HttpPost("/logout")]
		public async Task<IActionResult> Logout() {
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return LocalRedirect("/");
		}
	}
}

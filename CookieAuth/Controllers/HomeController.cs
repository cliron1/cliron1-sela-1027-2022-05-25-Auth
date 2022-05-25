using CookieAuth.Models;
using CookieAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookieAuth.Controllers {
	[Authorize]
	public class HomeController : Controller {
		private readonly IUserService userService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(IUserService userService, ILogger<HomeController> logger) {
			this.userService = userService;
			_logger = logger;
		}

		public IActionResult Index() {
			return View();
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCustomAuth.Controllers {
	[ApiController]
	[Route("[controller]")]
	[Authorize]
	public class WeatherForecastController : ControllerBase {
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger) {
			_logger = logger;
		}

		[HttpGet("GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get() {
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet("GetMyName")]
		public IActionResult GetMyName() {
			return Ok(new {
				User.Identity.Name,
				User.Identity.IsAuthenticated,
				Claims = User.Claims.Select(x => $"{x.Type} = {x.Value}")
			});
		}
	}
}
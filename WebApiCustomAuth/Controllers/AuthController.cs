using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiCustomAuth.Controllers {
	[ApiController]
	[Route("/")]
	public class AuthController: ControllerBase {
		[HttpPost("get-token")]
		public ActionResult<string> GetToken(AuthModel model) {
			if (model.Email != "a@a.com" || model.Pwd != "1234")
				return BadRequest();

			var claims = new List<Claim> {
				new Claim(ClaimTypes.NameIdentifier, "77"),
				new Claim(ClaimTypes.Name, "Liron")
			};

			var credentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HugeSecret!ForSelaGroup1027")),
				SecurityAlgorithms.HmacSha256Signature
			);
			var token = new JwtSecurityToken (
				"Liron@1027",
				"Mobile",
				claims,
				null,
				DateTime.Now.AddMinutes(120),
				credentials
			);

			var ret = new JwtSecurityTokenHandler()
				.WriteToken(token);
			return ret;
		}
	}

	public class AuthModel {
		public string Email { get; set; }
		
		public string Pwd { get; set; }
	}
}

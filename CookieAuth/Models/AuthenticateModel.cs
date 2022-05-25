using System.ComponentModel.DataAnnotations;

namespace CookieAuth.Models {
	public class AuthenticateModel {
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
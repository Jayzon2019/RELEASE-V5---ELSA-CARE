using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Identity.Features
{
	public class LoginInputModel
	{
		[Required]
		[DisplayName("Email")]
		[EmailAddress(ErrorMessage = "Invalid email address format.")]
		[MaxLength(256, ErrorMessage = "Email address must not exceed 256 characters.")]
		public string Username { get; set; }

		[Required]
		[DisplayName("Password")]
		[MinLength(8, ErrorMessage = "Password must be between 8 to 256 characters.")]
		[MaxLength(256, ErrorMessage = "Password must be between 8 to 256 characters.")]
		public string Password { get; set; }

		public bool RememberLogin { get; set; }

		public string ReturnUrl { get; set; }
	}
}

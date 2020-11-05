using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class UserPasswordViewModel
	{
		public UserPasswordViewModel()
		{
		}

		public string Id { get; set; }

		[Required]
		[DisplayName("Old Password")]
		[MinLength(8, ErrorMessage = "Password must be between 8 to 256 characters.")]
		[MaxLength(256, ErrorMessage = "Password must be between 8 to 256 characters.")]
		public string OldPassword { get; set; }

		[Required]
		[DisplayName("New Password")]
		[MinLength(8, ErrorMessage = "Password must be between 8 to 256 characters.")]
		[MaxLength(256, ErrorMessage = "Password must be between 8 to 256 characters.")]
		public string NewPassword1 { get; set; }

		[Required]
		[DisplayName("Retype New Password")]
		[Compare("NewPassword1", ErrorMessage = "The passwords didn't match.")]
		public string NewPassword2 { get; set; }
	}
}

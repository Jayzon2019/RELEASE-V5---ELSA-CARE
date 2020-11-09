using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class UserWithPasswordViewModel : UserViewModel
	{
		public UserWithPasswordViewModel()
		{
		}

		[Required]
		[DisplayName("Password")]
		[MinLength(8, ErrorMessage = "Password must be between 8 to 256 characters.")]
		[MaxLength(256, ErrorMessage = "Password must be between 8 to 256 characters.")]
		public string Password { get; set; }
	}
}

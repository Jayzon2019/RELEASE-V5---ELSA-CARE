using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
	public class UserViewModel
	{
		public Guid? Id { get; set; }

		[Required]
		[EmailAddress]
		[MaxLength(256)]
		[DisplayName("Email")]
		public string UserName { get; set; }

		[Required]
		[MaxLength(50)]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[MaxLength(50)]
		[DisplayName("Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[MaxLength(50)]
		[DisplayName("Last Name")]
		public string LastName { get; set; }
	}
}

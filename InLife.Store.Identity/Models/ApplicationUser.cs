using System;
using Microsoft.AspNetCore.Identity;

namespace InLife.Store.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public DateTimeOffset? DateActivated { get; set; }
	}
}

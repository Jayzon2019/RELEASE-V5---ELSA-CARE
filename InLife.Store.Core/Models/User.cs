using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Core.Models
{
	public class User : Entity<string>
	{
		public string UserName { get; set; }

		public string Email { get; set; }

		public string PhoneNumber { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public virtual ICollection<User_UserRole> Roles { get; set; }
	}
}

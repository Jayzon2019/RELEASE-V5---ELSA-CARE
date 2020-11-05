using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InLife.Store.Core.Models
{
	public class User_UserRole
	{
		public string UserId { get; set; }

		public string UserRoleId { get; set; }

		public virtual User User { get; set; }

		public virtual UserRole Role
		{
			get { return UserRole.FromId(UserRoleId); }
			set { UserRoleId = value.Id; }
		}
	}
}

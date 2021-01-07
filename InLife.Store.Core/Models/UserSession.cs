using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Core.Models
{
	public class UserSession : Entity<Guid>
	{
		//public string UserId { get; set; }
		public virtual User User { get; set; }

		public byte[] Value { get; set; }

		public DateTimeOffset? LastActivity { get; set; }

		public DateTimeOffset? Expires { get; set; }
	}
}

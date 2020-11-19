using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class ActivityLog : Entity<int>
	{
		//public string ActionPerfomed { get; set; }
		//public string ActivityDescription { get; set; }
		//public string IpAddress { get; set; }
		//public DateTimeOffset? ActivityDate { get; set; }

		//public string ActivityById { get; set; }
		//public virtual User ActivityBy { get; set; }

		public string Action { get; set; }
		public string Description { get; set; }

		public virtual User TransactionBy { get; set; }
		public string TransactionByName { get; set; }
		public DateTimeOffset? TransactionDate { get; set; }
		public string TransactionRemoteAddress { get; set; }
	}
}

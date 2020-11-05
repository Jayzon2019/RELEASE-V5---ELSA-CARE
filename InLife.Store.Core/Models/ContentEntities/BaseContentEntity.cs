using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public abstract class BaseContentEntity : Entity<int>
	{
		public bool IsActive { get; set; }
		public bool IsArchived { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public virtual User CreatedBy { get; set; }

		public DateTimeOffset? UpdatedDate { get; set; }
		public virtual User UpdatedBy { get; set; }
	}
}

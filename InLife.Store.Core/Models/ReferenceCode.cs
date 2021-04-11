using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class ReferenceCode : Entity<string>
	{
		public DateTime CreatedDate { get; set; }
	}
}

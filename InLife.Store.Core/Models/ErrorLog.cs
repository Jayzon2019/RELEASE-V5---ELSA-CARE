using System;

namespace InLife.Store.Core.Models
{
	public class ErrorLog : Entity<int>
	{
		public string Title { get; set; }

		public string Detail { get; set; }

		public string Source { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class KeyMetric : Entity<int>
	{
		public string PageName { get; set; }
		public int? PageViews { get; set; }
		public string Sessions { get; set; }
		public DateTimeOffset? PageViewedAt { get; set; }
		public DateTimeOffset? PageLeftAt { get; set; }
		public string MachineName { get; set; }
		public string Ip { get; set; }
	}
}

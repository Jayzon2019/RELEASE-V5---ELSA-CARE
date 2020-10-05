using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Faq : BaseContentEntity
	{
		public virtual FaqCategory Category { get; set; }

		public string Question { get; set; }
		public string Answer { get; set; }
		public int? SortNum { get; set; }
	}
}

using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class FaqCategory : BaseContentEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Faq> Faqs { get; set; }
	}
}

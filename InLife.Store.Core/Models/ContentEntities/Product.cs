using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Product : BaseContentEntity
	{
		public string ProductImg { get; set; }
		public string ProductName { get; set; }
		public string ProductPrice { get; set; }
		public string ProductCode { get; set; }
		public string ShortDescription { get; set; }
		public string PriceWithOffer { get; set; }
		public int? SortNum { get; set; }

		public virtual ICollection<ProductDetail> Details { get; set; }
	}
}

using System;

namespace InLife.Store.Api.Messages
{
	public class ProductResponse
	{
		public int Id { get; set; }

		public string ProductImg { get; set; }

		public string ProductName { get; set; }

		public string ProductPrice { get; set; }

		public string ProductCode { get; set; }

		public string ShortDescription { get; set; }

		public string PriceWithOffer { get; set; }

		public int? SortNum { get; set; } = 1000;
	}
}

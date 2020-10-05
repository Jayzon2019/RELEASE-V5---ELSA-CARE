using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
	public partial class ProductsViewModel : BaseContentViewModel
	{
		public string ProductImg { get; set; }

		[Required]
		[MaxLength(60)]
		[DisplayName("Name")]
		public string ProductName { get; set; }

		[Required]
		[MaxLength(10)]
		[DisplayName("Price")]
		public string ProductPrice { get; set; }

		[MaxLength(60)]
		[DisplayName("Code")]
		public string ProductCode { get; set; }

		public string ShortDescription { get; set; }

		public string PriceWithOffer { get; set; }

		[Required]
		[DisplayName("Sort Index")]
		public int? SortNum { get; set; } = 1000;
	}
}

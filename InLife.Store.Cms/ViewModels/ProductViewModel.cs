using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public partial class ProductViewModel : BaseContentViewModel
	{
		private readonly IProductRepository productRepository;

		public ProductViewModel(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public ProductViewModel(Product model) : base(model)
		{
			this.ProductImg = model.ProductImg;
			this.ProductName = model.ProductName;
			this.ProductPrice = model.ProductPrice;
			this.ProductCode = model.ProductCode;
			this.ShortDescription = model.ShortDescription;
			this.PriceWithOffer = model.PriceWithOffer;
			this.SortNum = model.SortNum;
		}

		public Product Map()
		{
			var model = this.productRepository.Get(Id);

			if (model == null)
				model = new Product();

			return this.Map(model);
		}

		public Product Map(Product model)
		{
			model.ProductImg = this.ProductImg;
			model.ProductName = this.ProductName;
			model.ProductPrice = this.ProductPrice;
			model.ProductCode = this.ProductCode;
			model.ShortDescription = this.ShortDescription;
			model.PriceWithOffer = this.PriceWithOffer;
			model.SortNum = this.SortNum;

			return model;
		}

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

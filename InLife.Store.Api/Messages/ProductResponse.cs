using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class ProductResponse : BaseResponse
	{
		public ProductResponse()
		{
		}

		public ProductResponse(Product model)
		{
			Id = model.Id;

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			ProductImg = ParseImageData(model.ProductImg);

			ProductName = model.ProductName;
			ProductPrice = model.ProductPrice;
			ProductCode = model.ProductCode;
			ShortDescription = model.ShortDescription;
			PriceWithOffer = model.PriceWithOffer;
			SortNum = model.SortNum;
		}

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

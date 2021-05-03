using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class PrimeHeroResponse : BaseResponse
	{
		public PrimeHeroResponse()
		{
		}

		public PrimeHeroResponse(PrimeHero model)
		{
			Id = model.Id;

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			PrimeHeroBg = ParseImageData(model.PrimeHeroBg);

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			PrimeHeroMobBg = ParseImageData(model.PrimeHeroMobBg);

			PrimeHeroTitle = model.PrimeHeroTitle;
			PrimeHeroBtnTxt = model.PrimeHeroBtnTxt;
			BtnTxtLink = model.BtnTxtLink;
			Heading = model.Heading;
			SubHeading = model.SubHeading;
			HeadingColor = model.HeadingColor;
			SubHeadingColor = model.SubHeadingColor;
			ContentPostion = model.ContentPostion;
		}

		public int Id { get; set; }

		public string PrimeHeroBg { get; set; }

		public string PrimeHeroTitle { get; set; }

		public string PrimeHeroBtnTxt { get; set; }

		public string BtnTxtLink { get; set; }

		public string Heading { get; set; }

		public string SubHeading { get; set; }

		public string PrimeHeroMobBg { get; set; }

		public string HeadingColor { get; set; }

		public string SubHeadingColor { get; set; }

		public string ContentPostion { get; set; }
	}
}

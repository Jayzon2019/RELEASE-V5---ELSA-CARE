using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class HeroResponse : BaseResponse
	{
		public HeroResponse()
		{
		}

		public HeroResponse(Hero model)
		{
			Id = model.Id;

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			HeroBg = ParseImageData(model.HeroBg);

			// This is a hack, old uploaded images doesn't have an image data
			// Clean this up when StoreFront has been updated
			HeroMobBg = ParseImageData(model.HeroMobBg);

			HeroTitle = model.HeroTitle;
			HeroBtnTxt = model.HeroBtnTxt;
			BtnTxtLink = model.BtnTxtLink;
			Heading = model.Heading;
			SubHeading = model.SubHeading;
			HeadingColor = model.HeadingColor;
			SubHeadingColor = model.SubHeadingColor;
			ContentPostion = model.ContentPostion;
		}

		public int Id { get; set; }

		public string HeroBg { get; set; }

		public string HeroTitle { get; set; }

		public string HeroBtnTxt { get; set; }

		public string BtnTxtLink { get; set; }

		public string Heading { get; set; }

		public string SubHeading { get; set; }

		public string HeroMobBg { get; set; }

		public string HeadingColor { get; set; }

		public string SubHeadingColor { get; set; }

		public string ContentPostion { get; set; }
	}
}

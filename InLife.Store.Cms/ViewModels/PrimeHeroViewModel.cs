using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class PrimeHeroViewModel : BaseContentViewModel
	{
		public PrimeHeroViewModel()
		{
		}

		public PrimeHeroViewModel(PrimeHero model) : base(model)
		{
			this.PrimeHeroBg = ParseImageData(model.PrimeHeroBg);
			this.PrimeHeroTitle = model.PrimeHeroTitle;
			this.PrimeHeroBtnTxt = model.PrimeHeroBtnTxt;
			this.BtnTxtLink = model.BtnTxtLink;
			this.Heading = model.Heading;
			this.SubHeading = model.SubHeading;
			this.PrimeHeroMobBg = ParseImageData(model.PrimeHeroMobBg);
			this.HeadingColor = model.HeadingColor;
			this.SubHeadingColor = model.SubHeadingColor;
			this.ContentPostion = model.ContentPostion;
		}

		public PrimeHero Map()
		{
			var model = new PrimeHero();
			return this.Map(model);
		}

		public PrimeHero Map(PrimeHero model)
		{
			model.PrimeHeroBg = ParseImageData(this.PrimeHeroBg);
			model.PrimeHeroTitle = this.PrimeHeroTitle;
			model.PrimeHeroBtnTxt = this.PrimeHeroBtnTxt;
			model.BtnTxtLink = this.BtnTxtLink;
			model.Heading = this.Heading;
			model.SubHeading = this.SubHeading;
			model.PrimeHeroMobBg = ParseImageData(this.PrimeHeroMobBg);
			model.HeadingColor = this.HeadingColor;
			model.SubHeadingColor = this.SubHeadingColor;
			model.ContentPostion = this.ContentPostion;

			return model;
		}


		[Required]
		public string PrimeHeroBg { get; set; }

		[Required]
		[MaxLength(60)]
		[DisplayName("Title")]
		public string PrimeHeroTitle { get; set; }

		[Required]
		[MaxLength(30)]
		[DisplayName("Button Text")]
		public string PrimeHeroBtnTxt { get; set; }

		[Required]
		[MaxLength(300)]
		[DisplayName("Button Link")]
		public string BtnTxtLink { get; set; }

		[Required]
		[MaxLength(300)]
		[DisplayName("Heading")]
		public string Heading { get; set; }

		[Required]
		[MaxLength(300)]
		[DisplayName("Sub-Heading")]
		public string SubHeading { get; set; }

		[Required]
		public string PrimeHeroMobBg { get; set; }

		public string HeadingColor { get; set; }

		public string SubHeadingColor { get; set; }

		public string ContentPostion { get; set; }
	}
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public class HeroViewModel : BaseContentViewModel
	{
		private readonly IHeroRepository heroRepository;

		public HeroViewModel(IHeroRepository heroRepository)
		{
			this.heroRepository = heroRepository;
		}

		public HeroViewModel(Hero model) : base(model)
		{
			this.HeroBg = model.HeroBg;
			this.HeroTitle = model.HeroTitle;
			this.HeroBtnTxt = model.HeroBtnTxt;
			this.BtnTxtLink = model.BtnTxtLink;
			this.Heading = model.Heading;
			this.SubHeading = model.SubHeading;
			this.HeroMobBg = model.HeroMobBg;
			this.HeadingColor = model.HeadingColor;
			this.SubHeadingColor = model.SubHeadingColor;
			this.ContentPostion = model.ContentPostion;
		}

		public Hero Map()
		{
			var model = this.heroRepository.Get(Id);

			if (model == null)
				model = new Hero();

			return this.Map(model);
		}

		public Hero Map(Hero model)
		{
			model.HeroBg = this.HeroBg;
			model.HeroTitle = this.HeroTitle;
			model.HeroBtnTxt = this.HeroBtnTxt;
			model.BtnTxtLink = this.BtnTxtLink;
			model.Heading = this.Heading;
			model.SubHeading = this.SubHeading;
			model.HeroMobBg = this.HeroMobBg;
			model.HeadingColor = this.HeadingColor;
			model.SubHeadingColor = this.SubHeadingColor;
			model.ContentPostion = this.ContentPostion;

			return model;
		}



		public string HeroBg { get; set; }

		[Required]
		[MaxLength(60)]
		[DisplayName("Title")]
		public string HeroTitle { get; set; }

		[Required]
		[MaxLength(30)]
		[DisplayName("Button Text")]
		public string HeroBtnTxt { get; set; }

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


		public string HeroMobBg { get; set; }

		public string HeadingColor { get; set; }

		public string SubHeadingColor { get; set; }

		public string ContentPostion { get; set; }
	}
}

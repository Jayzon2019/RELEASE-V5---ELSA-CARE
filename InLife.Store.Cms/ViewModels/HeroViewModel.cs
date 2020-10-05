using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
	public class HeroViewModel : BaseContentViewModel
	{
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

using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Hero : BaseContentEntity
	{
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

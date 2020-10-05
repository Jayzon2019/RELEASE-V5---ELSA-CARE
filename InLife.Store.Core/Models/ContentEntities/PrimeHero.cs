using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class PrimeHero : BaseContentEntity
	{
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

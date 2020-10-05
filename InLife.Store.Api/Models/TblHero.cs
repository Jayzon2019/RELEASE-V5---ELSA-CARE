using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblHero
    {
        public int HeroId { get; set; }
        public string HeroBg { get; set; }
        public string HeroTitle { get; set; }
        public string HeroBtnTxt { get; set; }
        public string BtnTxtLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string HeroMobBg { get; set; }
        public string HeadingColor { get; set; }
        public string SubHeadingColor { get; set; }
        public string ContentPostion { get; set; }
    }
}

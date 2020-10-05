using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblPrimeHero
    {
        public int PrimeHeroId { get; set; }
        public string PrimeHeroBg { get; set; }
        public string PrimeHeroTitle { get; set; }
        public string PrimeHeroBtnTxt { get; set; }
        public string BtnTxtLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public string PrimeHeroMobBg { get; set; }
        public string HeadingColor { get; set; }
        public string SubHeadingColor { get; set; }
        public string ContentPostion { get; set; }
    }
}

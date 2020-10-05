using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class PrimeHeroViewModel
    {
        [Key]
        public int intPrimeHeroId { get; set; }
        public string strPrimeHeroBg { get; set; }
        [Required(ErrorMessage = "Title is required!!!")]
        public string strPrimeHeroTitle { get; set; }
        [Required(ErrorMessage = "Button Text is required!!!")]
        public string strPrimeHeroBtnTxt { get; set; }
        [Required(ErrorMessage = "Button Link is required!!!")]
        public string strBtnTxtLink { get; set; }

        [Required(ErrorMessage = "Heading is required!!!")]
        public string strHeading { get; set; }
        [Required(ErrorMessage = "Sub-Heading is required!!!")]
        public string strSubHeading { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public string strCreatedByUser { get; set; }
        public int? intUpdatedBy { get; set; }
        public string strUpdatedByUser { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
        public string strPrimeHeroMobBg { get; set; }
        public string strHeadingColor { get; set; }
        public string strSubHeadingColor { get; set; }
        public string strContentPostion { get; set; }
    }
}

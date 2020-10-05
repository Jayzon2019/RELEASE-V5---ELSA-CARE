using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class FooterLinksViewModel
    {
        [Key]
        public int intFooterLinkId { get; set; }
        public string strLogoUrl { get; set; }
        public string strMainSiteUrl { get; set; }
        
        public string strInsCommissionUrl { get; set; }
        public string strCusCharterUrl { get; set; }
        public string strTermsConditionUrl { get; set; }
        public string strPrivacyPolicyUrl { get; set; }
        public string strContactUsUrl { get; set; }
        public string strFbUrl { get; set; }
        public DateTime? dteCreatedDate { get; set; }
        public int? intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool? blnIsActive { get; set; }
        public string strTweeterUrl { get; set; }
        public string strInstaUrl { get; set; }
        public string strYouTubeUrl { get; set; }
        public string strCreatedByUser { get; set; }
        public string strUpdatedByUser { get; set; }
        public string strMainSiteTxt { get; set; }
        public string strInsCommissionTxt { get; set; }
        public string strCusCharterTxt { get; set; }
        public string strTermsConditionTxt { get; set; }
        public string strPrivacyPolicyTxt { get; set; }
        public string strContactUsTxt { get; set; }
    }
}



















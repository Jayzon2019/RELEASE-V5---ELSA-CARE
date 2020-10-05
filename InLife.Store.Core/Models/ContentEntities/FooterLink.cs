using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
    public class FooterLink : BaseContentEntity
	{
        public string LogoUrl { get; set; }
        public string MainSiteUrl { get; set; }
        public string InsCommissionUrl { get; set; }
        public string CusCharterUrl { get; set; }
        public string TermsConditionUrl { get; set; }
        public string PrivacyPolicyUrl { get; set; }
        public string ContactUsUrl { get; set; }
        public string FbUrl { get; set; }
        public string TweeterUrl { get; set; }
        public string InstaUrl { get; set; }
        public string YouTubeUrl { get; set; }
        public string MainSiteTxt { get; set; }
        public string InsCommissionTxt { get; set; }
        public string CusCharterTxt { get; set; }
        public string TermsConditionTxt { get; set; }
        public string PrivacyPolicyTxt { get; set; }
        public string ContactUsTxt { get; set; }
    }
}

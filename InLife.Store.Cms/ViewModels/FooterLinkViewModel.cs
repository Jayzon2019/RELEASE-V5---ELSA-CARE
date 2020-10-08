using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Cms.ViewModels
{
	public partial class FooterLinkViewModel : BaseContentViewModel
	{
		private readonly IFooterLinkRepository footerLinkRepository;

		public FooterLinkViewModel(IFooterLinkRepository footerLinkRepository)
		{
			this.footerLinkRepository = footerLinkRepository;
		}

		public FooterLinkViewModel(FooterLink model) : base(model)
		{
			this.LogoUrl = model.LogoUrl;
			this.MainSiteUrl = model.MainSiteUrl;
			this.InsCommissionUrl = model.InsCommissionUrl;
			this.CusCharterUrl = model.CusCharterUrl;
			this.TermsConditionUrl = model.TermsConditionUrl;
			this.PrivacyPolicyUrl = model.PrivacyPolicyUrl;
			this.ContactUsUrl = model.ContactUsUrl;
			this.FbUrl = model.FbUrl;
			this.TweeterUrl = model.TweeterUrl;
			this.InstaUrl = model.InstaUrl;
			this.YouTubeUrl = model.YouTubeUrl;
			this.MainSiteTxt = model.MainSiteTxt;
			this.InsCommissionTxt = model.InsCommissionTxt;
			this.CusCharterTxt = model.CusCharterTxt;
			this.TermsConditionTxt = model.TermsConditionTxt;
			this.PrivacyPolicyTxt = model.PrivacyPolicyTxt;
			this.ContactUsTxt = model.ContactUsTxt;
		}

		public FooterLink Map()
		{
			var model = this.footerLinkRepository.Get(Id);

			if (model == null)
				model = new FooterLink();

			return this.Map(model);
		}

		public FooterLink Map(FooterLink model)
		{
			model.LogoUrl = this.LogoUrl;
			model.MainSiteUrl = this.MainSiteUrl;
			model.InsCommissionUrl = this.InsCommissionUrl;
			model.CusCharterUrl = this.CusCharterUrl;
			model.TermsConditionUrl = this.TermsConditionUrl;
			model.PrivacyPolicyUrl = this.PrivacyPolicyUrl;
			model.ContactUsUrl = this.ContactUsUrl;
			model.FbUrl = this.FbUrl;
			model.TweeterUrl = this.TweeterUrl;
			model.InstaUrl = this.InstaUrl;
			model.YouTubeUrl = this.YouTubeUrl;
			model.MainSiteTxt = this.MainSiteTxt;
			model.InsCommissionTxt = this.InsCommissionTxt;
			model.CusCharterTxt = this.CusCharterTxt;
			model.TermsConditionTxt = this.TermsConditionTxt;
			model.PrivacyPolicyTxt = this.PrivacyPolicyTxt;
			model.ContactUsTxt = this.ContactUsTxt;

			return model;
		}


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



















using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;

using InLife.Store.Api.Messages;

namespace InLife.Store.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[Produces(MediaTypeNames.Application.Json)]
	public class FooterLinksController : BaseController
	{
		private readonly IFooterLinkRepository footerLinkRepository;

		public FooterLinksController
		(
			ILogger<BaseController> logger,
			IFooterLinkRepository footerLinkRepository
		) : base
		(
			logger
		)
		{
			this.footerLinkRepository = footerLinkRepository;
		}


		[HttpGet]
		[Route("GetFooterLinks")]
		public IActionResult GetFooterLinks()
		{
			try
			{
				var result = footerLinkRepository
					.GetAll()
					.Select(model => new FooterLinkResponse
					{
						Id = model.Id,
						LogoUrl = model.LogoUrl,
						MainSiteUrl = model.MainSiteUrl,
						InsCommissionUrl = model.InsCommissionUrl,
						CusCharterUrl = model.CusCharterUrl,
						TermsConditionUrl = model.TermsConditionUrl,
						PrivacyPolicyUrl = model.PrivacyPolicyUrl,
						ContactUsUrl = model.ContactUsUrl,
						FbUrl = model.FbUrl,
						TweeterUrl = model.TweeterUrl,
						InstaUrl = model.InstaUrl,
						YouTubeUrl = model.YouTubeUrl,
						MainSiteTxt = model.MainSiteTxt,
						InsCommissionTxt = model.InsCommissionTxt,
						CusCharterTxt = model.CusCharterTxt,
						TermsConditionTxt = model.TermsConditionTxt,
						PrivacyPolicyTxt = model.PrivacyPolicyTxt,
						ContactUsTxt = model.ContactUsTxt
					})
					.ToList();

				return Ok(result);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}

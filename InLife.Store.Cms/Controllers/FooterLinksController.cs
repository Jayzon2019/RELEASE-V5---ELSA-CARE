using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Cms.ViewModels;

namespace InLife.Store.Cms.Controllers
{
	[Authorize]
	public class FooterLinksController : BaseController
	{
		private readonly IFooterLinkRepository footerLinkRepository;

		public FooterLinksController
		(
			ILogger<FooterLinksController> logger,
			IUserRepository userRepository,
			IFooterLinkRepository footerLinkRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.footerLinkRepository = footerLinkRepository;
		}

		// GET: FooterLinks
		public ActionResult Index()
		{
			try
			{
				var viewModelList = footerLinkRepository.GetAll().Select(item => new FooterLinkViewModel
				{
					LogoUrl = item.LogoUrl,
					MainSiteUrl = item.MainSiteUrl,
					InsCommissionUrl = item.InsCommissionUrl,
					CusCharterUrl = item.CusCharterUrl,
					TermsConditionUrl = item.TermsConditionUrl,
					PrivacyPolicyUrl = item.PrivacyPolicyUrl,
					ContactUsUrl = item.ContactUsUrl,
					FbUrl = item.FbUrl,
					TweeterUrl = item.TweeterUrl,
					InstaUrl = item.InstaUrl,
					YouTubeUrl = item.YouTubeUrl,
					MainSiteTxt = item.MainSiteTxt,
					InsCommissionTxt = item.InsCommissionTxt,
					CusCharterTxt = item.CusCharterTxt,
					TermsConditionTxt = item.TermsConditionTxt,
					PrivacyPolicyTxt = item.PrivacyPolicyTxt,
					ContactUsTxt = item.ContactUsTxt,

					CreatedBy = (item.CreatedBy == null)
						? (Guid?)null
						: item.CreatedBy.Id,
					CreatedByName = (item.CreatedBy == null)
						? null
						: $"{item.CreatedBy.FirstName} {item.CreatedBy.LastName}".Trim(),
					CreatedDate = item.CreatedDate,

					UpdatedBy = (item.UpdatedBy == null)
						? (Guid?)null
						: item.UpdatedBy.Id,
					UpdatedByName = (item.UpdatedBy == null)
						? null
						: $"{item.UpdatedBy.FirstName} {item.UpdatedBy.LastName}".Trim(),
					UpdatedDate = item.UpdatedDate
				}).ToList();

				if (viewModelList == null)
					return NotFound();

				return View(viewModelList);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: FooterLinks/Details/5
		public ActionResult Details()
		{
			var log = "";
			try
			{

				var footerLink = FLS.GetFooterLink(ref log);
				if (footerLink == null)
				{
					return NotFound();
				}

				return View(footerLink);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}
		}

		// GET: FooterLinks/Create
		public IActionResult Create()
		{
			var log = "";
			try
			{

				return View();
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}
		}

		// POST: FooterLinks/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("intFooterLinkId,strLogoUrl,strMainSiteUrl,strInsCommissionUrl,strCusCharterUrl,strTermsConditionUrl,strPrivacyPolicyUrl,strContactUsUrl,strFbUrl,strTweeterUrl,strInstaUrl,strYouTubeUrl,strMainSiteTxt , strInsCommissionTxt, strCusCharterTxt, strTermsConditionTxt,  strPrivacyPolicyTxt,   strContactUsTxt ")] FooterLinksViewModel footerLinksViewModel)
		{
			var log = "";
			try
			{
				if (ModelState.IsValid)
				{
					var isSaved = FLS.SaveFooterLink(ref log, footerLinksViewModel);
					if (isSaved == "Saved")
					{
						return RedirectToAction(nameof(Index));
					}
					else
					{
						ViewBag.error = Comman.SomethingWntWrong;
					}
				}
				return View(footerLinksViewModel);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return View();
			}
		}

		// GET: FooterLinks/Edit/5
		public ActionResult Edit(int? id)
		{
			var log = "";
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				var footerVM = FLS.GetFooterLinkById(ref log, id);
				if (footerVM == null)
				{
					return NotFound();
				}
				return View(footerVM);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return NotFound();
			}
		}

		// POST: FooterLinks/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("intFooterLinkId,strLogoUrl,strMainSiteUrl,strInsCommissionUrl,strCusCharterUrl,strTermsConditionUrl,strPrivacyPolicyUrl,strContactUsUrl,strFbUrl,strTweeterUrl,strInstaUrl,strYouTubeUrl,strMainSiteTxt , strInsCommissionTxt, strCusCharterTxt, strTermsConditionTxt,  strPrivacyPolicyTxt,   strContactUsTxt ")] FooterLinksViewModel footerLinksViewModel)
		{
			var log = "";
			if (ModelState.IsValid)
			{
				try
				{
					if (id != footerLinksViewModel.intFooterLinkId)
					{
						return NotFound();
					}
					var updateFooterLink = FLS.EditFooterLink(ref log, footerLinksViewModel);
					if (updateFooterLink == "Updated")
					{
						return RedirectToAction(nameof(Details));
					}
					else
					{
						ViewBag.error = Comman.SomethingWntWrong;
						var footerVM = FLS.GetFooterLinkById(ref log, id);
						return View(footerVM);
					}
				}
				catch (Exception ex)
				{
					string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
					var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
					lR.SaveExceptionLogs(exLog, ex, methodName);
					ViewBag.error = Comman.SomethingWntWrong;
					var footerVM = FLS.GetFooterLinkById(ref log, id);
					return View(footerVM);
				}
			}
			return View(footerLinksViewModel);

		}

		public ActionResult Delete(int id)
		{
			var log = "";
			try
			{
				FLS.Deactivate_DeleteFooterLink(ref log, id);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				ViewBag.error = Comman.SomethingWntWrong;
				return RedirectToAction(nameof(Index));
			}
		}

	}
}

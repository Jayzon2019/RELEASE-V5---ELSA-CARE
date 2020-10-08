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
				var viewModelList = footerLinkRepository
					.GetAll()
					.Select(model => new FooterLinkViewModel(model))
					.ToList();

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
		public ActionResult Details(int? id)
		{
			try
			{
				var model = footerLinkRepository.Get(id);

				if (model == null)
					return NotFound();

				var viewModel = new FooterLinkViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: FooterLinks/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: FooterLinks/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("LogoUrl, MainSiteUrl, InsCommissionUrl, CusCharterUrl, TermsConditionUrl, PrivacyPolicyUrl, ContactUsUrl, FbUrl, TweeterUrl, InstaUrl, YouTubeUrl, MainSiteTxt, InsCommissionTxt, CusCharterTxt, TermsConditionTxt, PrivacyPolicyTxt, ContactUsTxt")] FooterLinkViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.footerLinkRepository.Create(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: FooterLinks/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.footerLinkRepository.Get(id);
				if (model == null)
					return NotFound();

				var viewModel = new FooterLinkViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: FooterLinks/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("LogoUrl, MainSiteUrl, InsCommissionUrl, CusCharterUrl, TermsConditionUrl, PrivacyPolicyUrl, ContactUsUrl, FbUrl, TweeterUrl, InstaUrl, YouTubeUrl, MainSiteTxt, InsCommissionTxt, CusCharterTxt, TermsConditionTxt, PrivacyPolicyTxt, ContactUsTxt")] FooterLinkViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				viewModel.Id = id;
				var model = viewModel.Map();
				if (model.Id == default)
					return NotFound();

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.footerLinkRepository.Update(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		public ActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.footerLinkRepository.Get(id);
				if (model == null)
					return NotFound();

				this.footerLinkRepository.Delete(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}
	}
}

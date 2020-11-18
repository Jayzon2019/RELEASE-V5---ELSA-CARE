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
	public class HeroController : BaseController
	{
		private readonly IHeroRepository heroRepository;

		public HeroController
		(
			ILogger<HeroController> logger,
			IUserRepository userRepository,
			IActivityLogRepository activityLogRepository,
			IHeroRepository heroRepository
		) : base
		(
			userRepository,
			logger,
			activityLogRepository
		)
		{
			this.heroRepository = heroRepository;
		}

		// GET: Hero
		public ActionResult Index()
		{
			try
			{
				var viewModelList = heroRepository
					.GetAll()
					.Select(model => new HeroViewModel(model))
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

		// GET: Hero/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				//var model = heroRepository.Get(id);
				var model = heroRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new HeroViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Hero/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Hero/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("HeroBg, HeroMobBg, HeroTitle, HeroBtnTxt, BtnTxtLink, Heading, SubHeading, HeadingColor, SubHeadingColor, ContentPostion")] HeroViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.heroRepository.Create(model);

				LogUserActivity("Created a Hero", $"Created a new Hero - {model.HeroTitle}");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Hero/Edit/5
		public IActionResult Edit(int? id)
		{
			try
			{
				var model = this.heroRepository.Get(id);

				if (model == null)
					return NotFound();

				var viewModel = new HeroViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Hero/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("HeroBg, HeroMobBg, HeroTitle, HeroBtnTxt, BtnTxtLink, Heading, SubHeading, HeadingColor, SubHeadingColor, ContentPostion")] HeroViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.heroRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.heroRepository.Update(model);

				LogUserActivity("Updated a Hero", $"Hero '{model.HeroTitle}' [{model.Id}] has been updated.");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}


		// POST: Users/Delete/5
		// [HttpPost, ActionName("Delete")]
		// [ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.heroRepository.Get(id);
				if (model == null)
					return NotFound();

				this.heroRepository.Delete(model);

				LogUserActivity("Deleted a Hero", $"Hero '{model.HeroTitle}' [{model.Id}] has been deleted.");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		// [HttpPost, ActionName("Deactive")]
		// [ValidateAntiForgeryToken]
		//public ActionResult DeactiveHero(int id)
		//{
		//	var log = "";
		//	try
		//	{
		//		HS.Deactivate_DeleteHeroSlider(ref log, id, false);
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch (Exception ex)
		//	{
		//		string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
		//		var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
		//		lR.SaveExceptionLogs(exLog, ex, methodName);
		//		ViewBag.error = Comman.SomethingWntWrong;
		//		return RedirectToAction(nameof(Index));
		//	}
		//}
	}
}

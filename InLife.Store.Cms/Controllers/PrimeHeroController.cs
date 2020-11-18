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
	public class PrimeHeroController : BaseController
	{
		private readonly IPrimeHeroRepository primeHeroRepository;

		public PrimeHeroController
		(
			ILogger<PrimeHeroController> logger,
			IUserRepository userRepository,
			IActivityLogRepository activityLogRepository,
			IPrimeHeroRepository primeHeroRepository
		) : base
		(
			userRepository,
			logger,
			activityLogRepository
		)
		{
			this.primeHeroRepository = primeHeroRepository;
		}

		public ActionResult Index()
		{
			try
			{
				var viewModelList = primeHeroRepository
					.GetAll()
					.Select(model => new PrimeHeroViewModel(model))
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

		// GET: PrimeHero/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				//var model = primeHeroRepository.Get(id);
				var model = primeHeroRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new PrimeHeroViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: PrimeHero/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PrimeHero/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("PrimeHeroBg, PrimeHeroMobBg, PrimeHeroTitle, PrimeHeroBtnTxt, BtnTxtLink, Heading, SubHeading, HeadingColor, SubHeadingColor, ContentPostion")] PrimeHeroViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.primeHeroRepository.Create(model);

				LogUserActivity("Created a Prime Hero Banner", $"Created a new Prime Hero Banner - {model.PrimeHeroTitle}");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: PrimeHero/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.primeHeroRepository.Get(id);
				if (model == null)
					return NotFound();

				var viewModel = new PrimeHeroViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: PrimeHero/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("PrimeHeroBg, PrimeHeroTitle, PrimeHeroBtnTxt, BtnTxtLink, Heading, SubHeading, HeadingColor, SubHeadingColor, ContentPostion")] PrimeHeroViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.primeHeroRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.primeHeroRepository.Update(model);

				LogUserActivity("Updated a Prime Hero Banner", $"Prime Hero Banner '{model.PrimeHeroTitle}' [{model.Id}] has been updated.");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Delete")]
		// [ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.primeHeroRepository.Get(id);
				if (model == null)
					return NotFound();

				this.primeHeroRepository.Delete(model);

				LogUserActivity("Deleted a Prime Hero Banner", $"Prime Hero Banner '{model.PrimeHeroTitle}' [{model.Id}] has been deleted.");

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
		//		PHS.Deactivate_DeletePrimeHeroSlider(ref log, id, false);
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

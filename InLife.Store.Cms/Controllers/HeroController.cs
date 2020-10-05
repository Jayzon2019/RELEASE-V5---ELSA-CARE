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
	public class HeroController : BaseController
	{
		private readonly IHeroRepository heroRepository;

		public HeroController
		(
			ILogger<HeroController> logger,
			IUserRepository userRepository,
			IHeroRepository heroRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.heroRepository = heroRepository;
		}



		private IWebHostEnvironment _webHostEnvironment;
		public HeroController(IWebHostEnvironment hostingEnvironment)
		{
			_webHostEnvironment = hostingEnvironment;
		}
		HeroService HS = new HeroService();
		LogsRepo lR = new LogsRepo();


		// GET: Hero
		public ActionResult Index()
		{
			var log = "";
			try
			{
				var heroList = HS.GetHeroSliders(ref log);
				if (heroList != null)
				{
					return View(heroList);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
			}
		}

		// GET: Hero/Details/5
		public ActionResult Details(int? id)
		{
			var log = "";
			try
			{
				if (id == null)
				{
					return NotFound();
				}

				var HeroSlider = HS.GetHeroSliderById(ref log, id);
				if (HeroSlider == null)
				{
					return NotFound();
				}

				return View(HeroSlider);
			}
			catch (Exception ex)
			{
				string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
				var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
				lR.SaveExceptionLogs(exLog, ex, methodName);
				return NotFound();
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
		public IActionResult Create([Bind("intHeroId,strHeroBg,strHeroTitle,strHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] HeroViewModel heroViewModel)
		{
			var log = "";
			try
			{
				if (ModelState.IsValid)
				{
					siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
					var isSaved = HS.SaveHeroSlider(ref log, heroViewModel);
					if (isSaved != "Saved")
					{
						ViewBag.error = isSaved;
					}
					return RedirectToAction(nameof(Index));
				}
				return View(heroViewModel);
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

		// GET: Hero/Edit/5
		public IActionResult Edit(int? id)
		{
			var log = "";
			try
			{
				if (id == null)
				{
					return NotFound();
				}
				var heroVM = HS.GetHeroSliderById(ref log, id);
				if (heroVM == null)
				{
					return NotFound();
				}
				return View(heroVM);
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

		// POST: Hero/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("intHeroId,strHeroBg,strHeroTitle,strHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] HeroViewModel heroViewModel)
		{
			var log = "";
			if (ModelState.IsValid)
			{
				try
				{
					if (id != heroViewModel.intHeroId)
					{
						return View();
					}
					siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
					HS.EditHeroSlider(ref log, heroViewModel);
				}
				catch (Exception ex)
				{
					string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
					var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
					lR.SaveExceptionLogs(exLog, ex, methodName);
					ViewBag.error = Comman.SomethingWntWrong;
					return View();
				}
				return RedirectToAction(nameof(Index));
			}
			return View(heroViewModel);

		}


		// POST: Users/Delete/5
		// [HttpPost, ActionName("Delete")]
		// [ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			var log = "";
			try
			{
				HS.Deactivate_DeleteHeroSlider(ref log, id, true);
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

		// POST: Users/Delete/5
		// [HttpPost, ActionName("Deactive")]
		// [ValidateAntiForgeryToken]
		public ActionResult DeactiveHero(int id)
		{
			var log = "";
			try
			{
				HS.Deactivate_DeleteHeroSlider(ref log, id, false);
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

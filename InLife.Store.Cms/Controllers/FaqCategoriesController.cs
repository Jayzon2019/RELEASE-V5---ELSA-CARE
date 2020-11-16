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
	public class FaqCategoriesController : BaseController
	{
		private readonly IFaqCategoryRepository faqCategoryRepository;

		public FaqCategoriesController
		(
			ILogger<FaqCategoriesController> logger,
			IUserRepository userRepository,
			IFaqCategoryRepository faqCategoryRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.faqCategoryRepository = faqCategoryRepository;
		}

		// GET: FaqCategories
		public ActionResult Index()
		{
			try
			{
				var viewModelList = faqCategoryRepository
					.GetAll()
					.Select(model => new FaqCategoryViewModel(model))
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

		// GET: FaqCategories/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				//var model = faqCategoryRepository.Get(id);
				var model = faqCategoryRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new FaqCategoryViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: FaqCategories/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: FaqCategories/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("Name, Description")] FaqCategoryViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.faqCategoryRepository.Create(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: FaqCategories/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.faqCategoryRepository.Get(id);
				if (model == null)
					return NotFound();

				var viewModel = new FaqCategoryViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: FaqCategories/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("Name, Description")] FaqCategoryViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.faqCategoryRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.faqCategoryRepository.Update(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.faqCategoryRepository.Get(id);
				if (model == null)
					return NotFound();

				this.faqCategoryRepository.Delete(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Delete/5
		//[HttpPost, ActionName("Deactive")]
		// [ValidateAntiForgeryToken]
		//public ActionResult DeactiveHero(int id)
		//{
		//	var log = "";
		//	try
		//	{
		//		FC.Deactivate_DeleteFaqCategory(ref log, id, false);
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

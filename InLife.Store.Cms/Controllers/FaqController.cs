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
	public class FaqController : BaseController
	{
		private readonly IFaqCategoryRepository faqCategoryRepository;
		private readonly IFaqRepository faqRepository;

		public FaqController
		(
			ILogger<FaqController> logger,
			IUserRepository userRepository,
			IFaqCategoryRepository faqCategoryRepository,
			IFaqRepository faqRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.faqCategoryRepository = faqCategoryRepository;
			this.faqRepository = faqRepository;
		}

		// GET: Faq
		public ActionResult Index()
		{
			try
			{
				var viewModelList = faqRepository
					.GetAll()
					.Select(model => new FaqViewModel(model))
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

		// GET: Faq/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				//var model = faqRepository.Get(id);
				var model = faqRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new FaqViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Faq/Create
		public IActionResult Create()
		{
			try
			{
				var faqCategoryViewModelList = faqCategoryRepository
					.GetAll()
					.Select(model => new FaqCategoryViewModel(model))
					.ToList();

				ViewBag.FaqCategories = faqCategoryViewModelList;
				return View();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Faq/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("CategoryId, Question, Answer, SortNum")] FaqViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();

				var category = this.faqCategoryRepository.Get(viewModel.CategoryId);
				model.Category = category;
				
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.faqRepository.Create(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Faq/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.faqRepository.Get(id);
				if (model == null)
					return NotFound();

				var faqCategoryViewModelList = faqCategoryRepository
					.GetAll()
					.Select(model => new FaqCategoryViewModel(model))
					.ToList();

				ViewBag.FaqCategories = faqCategoryViewModelList;

				var viewModel = new FaqViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Faq/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("CategoryId, Question, Answer, SortNum")] FaqViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.faqRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);

				var category = this.faqCategoryRepository.Get(viewModel.CategoryId);
				model.Category = category;

				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.faqRepository.Update(model);

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
				var model = this.faqRepository.Get(id);
				if (model == null)
					return NotFound();

				this.faqRepository.Delete(model);

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
		//		FS.Deactivate_DeleteFaq(ref log, id, false);
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

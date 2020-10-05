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
				var viewModelList = faqRepository.GetAll().Select(item => new FaqViewModel
				{
					CategoryId = item.Category.Id,
					CategoryName = item.Category.Name,
					Question = item.Question,
					Answer = item.Answer,
					SortNum = item.SortNum,

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
				})
				.OrderBy(x => x.SortNum)
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
				var item = faqRepository.Get(id);

				if (item == null)
					return NotFound();

				return View(item);
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
				var list = faqCategoryRepository.GetAll();

				if (list == null)
					return NotFound();

				ViewBag.FaqCategories = list;
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
		public ActionResult Create([Bind("CategoryId, Question, Answer, SortNum")] FaqViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var category = this.faqCategoryRepository.Get(model.CategoryId);
				if (category == null)
					return NotFound();

				var user = this.CurrentUser();
				var item = new Faq
				{
					Category = category,
					Question = model.Question,
					Answer = model.Answer,
					SortNum = model.SortNum,
					CreatedBy = user,
					CreatedDate = DateTimeOffset.Now
				};

				this.faqRepository.Create(item);

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
				var item = this.faqRepository.Get(id);
				if (item == null)
					return NotFound();

				var categories = this.faqCategoryRepository.GetAll();
				ViewBag.FaqCategories = categories;

				var viewModel = new FaqViewModel
				{
					Question = item.Question,
					Answer = item.Answer,
					SortNum = item.SortNum,

					CategoryId = item.Category.Id,
					CategoryName = item.Category.Name,

					CreatedBy = item.CreatedBy.Id,
					CreatedDate = item.CreatedDate,
					CreatedByName = $"{item.CreatedBy?.FirstName} {item.CreatedBy?.LastName}".Trim(),
					UpdatedBy = item.UpdatedBy.Id,
					UpdatedDate = item.UpdatedDate,
					UpdatedByName = $"{item.UpdatedBy?.FirstName} {item.UpdatedBy?.LastName}".Trim()
				};

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
				return BadRequest(ModelState);

			try
			{
				var model = this.faqRepository.Get(id);
				if (model == null)
					return NotFound();

				var categoryModel = this.faqCategoryRepository.Get(viewModel.CategoryId);
				if (categoryModel == null)
					return NotFound();

				var user = this.CurrentUser();

				model.Question = viewModel.Question;
				model.Answer = viewModel.Answer;
				model.Category = categoryModel;
				model.UpdatedBy = user;
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

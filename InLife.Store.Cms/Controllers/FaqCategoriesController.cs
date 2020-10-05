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
				var viewModelList = faqCategoryRepository.GetAll().Select(item => new FaqCategoryViewModel
				{
					Name = item.Name,
					Description = item.Description,

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
				var model = faqCategoryRepository.Get(id);

				if (model == null)
					return NotFound();

				return View(item);
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
		public ActionResult Create([Bind("Name, Description")] FaqCategoryViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var user = this.CurrentUser();
				var item = new FaqCategory
				{
					Name = model.Name,
					Description = model.Description,
					CreatedBy = user,
					CreatedDate = DateTimeOffset.Now
				};

				this.faqCategoryRepository.Create(item);

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

				var viewModel = new FaqCategoryViewModel
				{
					Name = model.Name,
					Description = model.Description,
					CreatedBy = model.CreatedBy.Id,
					CreatedDate = model.CreatedDate,
					CreatedByName = $"{model.CreatedBy?.FirstName} {model.CreatedBy?.LastName}".Trim(),
					UpdatedBy = model.UpdatedBy.Id,
					UpdatedDate = model.UpdatedDate,
					UpdatedByName = $"{model.UpdatedBy?.FirstName} {model.UpdatedBy?.LastName}".Trim()
				};

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
				return BadRequest(ModelState);

			try
			{
				var model = this.faqCategoryRepository.Get(id);
				if (model == null)
					return NotFound();

				var user = this.CurrentUser();

				model.Name = viewModel.Name;
				model.Description = viewModel.Description;
				model.UpdatedBy = user;
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

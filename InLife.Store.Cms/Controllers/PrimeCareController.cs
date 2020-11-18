using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Cms.ViewModels;

using InLife.Store.Cms.Helpers;

namespace InLife.Store.Cms.Controllers
{
	public class PrimeCareController : BaseController
	{
		private readonly IPrimeCareRepository primeCareRepository;

		public PrimeCareController
		(
			ILogger<PrimeCareController> logger,
			IUserRepository userRepository,
			IActivityLogRepository activityLogRepository,
			IPrimeCareRepository primeCareRepository
		) : base
		(
			userRepository,
			logger,
			activityLogRepository
		)
		{
			this.primeCareRepository = primeCareRepository;
		}

		// GET: PrimeCare
		public ActionResult Index()
		{
			try
			{
				var viewModelList = primeCareRepository
					.GetAll()
					.Select(model => new PrimeCareViewModel(model))
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

		// GET: PrimeCare/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				//var model = primeCareRepository.Get(id);
				var model = primeCareRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new PrimeCareViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: PrimeCare/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PrimeCare/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("PrimeCareFile, PrimeCareFileName, PrimeCareFileDescription, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, IsArchived")] PrimeCareViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				model.CreatedBy = this.CurrentUser();
				model.CreatedDate = DateTimeOffset.Now;

				this.primeCareRepository.Create(model);

				LogUserActivity("Uploaded a PrimeCare file", $"Uploaded a new PrimeCare files - {model.PrimeCareFileName}");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: PrimeCare/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.primeCareRepository.Get(id);
				if (model == null)
					return NotFound();

				var viewModel = new PrimeCareViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: PrimeCare/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, [Bind("PrimeCareFile, PrimeCareFileName, PrimeCareFileDescription, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, IsArchived")] PrimeCareViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = this.primeCareRepository.Get(id);

				if (model == null)
					return NotFound();

				model = viewModel.Map(model);
				
				model.UpdatedBy = this.CurrentUser();
				model.UpdatedDate = DateTimeOffset.Now;

				this.primeCareRepository.Update(model);

				LogUserActivity("Updated a PrimeCare file", $"PrimeCare file '{model.PrimeCareFileName}' [{model.Id}] has been updated.");

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
				var model = this.primeCareRepository.Get(id);
				if (model == null)
					return NotFound();

				this.primeCareRepository.Delete(model);

				LogUserActivity("Deleted a PrimeCare file", $"PrimeCare file '{model.PrimeCareFileName}' [{model.Id}] has been deleted.");

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
		//		PCS.Deactivate_DeletePrimeCareFile(ref log, id, false);
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

		[HttpGet]
		public ActionResult GetPdf(string path)
		{
			return View();
			//var log = "";
			//try
			//{
			//	string filePath = path;
			//	var fileName = path.Split('\\').Last();
			//	Response.Headers.Add("Content-Disposition", "inline; " + fileName + " ");
			//	return File(filePath, "application/pdf");
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	ViewBag.error = Comman.SomethingWntWrong;
			//	return RedirectToAction(nameof(Index));
			//}
		}
	}
}

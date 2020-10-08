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
	[Authorize(Roles = "Admin")]
	public class UsersController : BaseController
	{
		//private readonly IUserRoleRepository userRoleRepository;

		public UsersController
		(
			ILogger<UsersController> logger,
			IUserRepository userRepository
		) : base
		(
			userRepository,
			logger
		)
		{

		}

		// GET: Users
		public ActionResult Index()
		{
			try
			{
				var viewModelList = userRepository
					.GetAll()
					.Select(model => new UserViewModel(model))
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

		// GET: Users/Details/5
		public ActionResult Details(int? id)
		{
			try
			{
				var model = userRepository.Get(id);

				if (model == null)
					return NotFound();

				var viewModel = new UserViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Users/Create
		public IActionResult Create()
		{
			return View();

			//var log = "";
			//try
			//{
			//	var genderList = US.GetGenders(ref log);
			//	ViewBag.genList = genderList;
			//	var roleList = US.GetRoles(ref log);
			//	ViewBag.roleList = roleList;

			//	return View();

			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	ViewBag.error = Comman.SomethingWntWrong;
			//	return NotFound();
			//}
		}

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("FirstName, LastName")] UserViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = viewModel.Map();
				//model.CreatedBy = this.CurrentUser();
				//model.CreatedDate = DateTimeOffset.Now;

				this.userRepository.Create(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Users/Edit/5
		public ActionResult Edit(int? id)
		{
			try
			{
				var model = this.userRepository.Get(id);
				if (model == null)
					return NotFound();

				var viewModel = new UserViewModel(model);

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid? id, [Bind("FirstName, LastName")] UserViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				viewModel.Id = id;
				var model = viewModel.Map();
				if (model.Id == default)
					return NotFound();

				//model.UpdatedBy = this.CurrentUser();
				//model.UpdatedDate = DateTimeOffset.Now;

				this.userRepository.Update(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}


		// POST: Users/Delete/5
		// [ValidateAntiForgeryToken]
		public ActionResult Delete(int? id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var model = this.userRepository.Get(id);
				if (model == null)
					return NotFound();

				this.userRepository.Delete(model);

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
		//public ActionResult Deactive(int id)
		//{
		//	var log = "";
		//	try
		//	{
		//		US.Deactivate_DeleteUser(ref log, id, false);
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

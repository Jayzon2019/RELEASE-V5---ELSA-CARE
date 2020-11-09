using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Services;

using InLife.Store.Cms.ViewModels;
using InLife.Store.Cms.Data;
using InLife.Store.Cms.Models;

namespace InLife.Store.Cms.Controllers
{
	//[Authorize(Roles = "Admin")]
	[Authorize]
	public class UsersController : BaseController
	{
		//private readonly IUserRoleRepository userRoleRepository;

		private readonly UserManager<ApplicationUser> userManager;
		private readonly IEmailService emailService;

		public UsersController
		(
			UserManager<ApplicationUser> userManager,
			IEmailService emailService,
			ILogger<UsersController> logger,
			IUserRepository userRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.userManager = userManager;
			this.emailService = emailService;
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
		public ActionResult Details(string id)
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
		public async Task<ActionResult> Create([Bind("Password, Email, FirstName, LastName")] UserWithPasswordViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var email = viewModel.Email.ToLower().Trim();
				var model = new ApplicationUser()
				{
					UserName = email,
					Email = email,
					FirstName = viewModel.FirstName.Trim(),
					LastName = viewModel.LastName.Trim()
				};

				//var tempPassword = Guid.NewGuid().ToString("N");

				//var createUserResult = await userManager.CreateAsync(model, tempPassword);
				var createUserResult = await userManager.CreateAsync(model, viewModel.Password);
				if (!createUserResult.Succeeded)
				{
					// Failed to create user
					return ErrorResult
					(
						status: StatusCodes.Status400BadRequest,
						title: $"Failed to create a new user",
						detail: $"There's error in creating the user with an email address of {email}."
					);
				}

				//var recipient = new MailAddress(email, $"{model.FirstName} {model.LastName}");
				//await emailService.SendPasswordAsync(recipient, tempPassword);
			
				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// GET: Users/Edit/5
		public ActionResult Edit(string id)
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
		public async Task<ActionResult> Edit(string id, [Bind("Email, FirstName, LastName")] UserViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var model = await userManager.FindByIdAsync(id);

				if (model == null)
					return NotFound();

				var email = viewModel.Email.ToLower().Trim();
				model.UserName = email;
				model.Email = email;
				model.FirstName = viewModel.FirstName.Trim();
				model.LastName = viewModel.LastName.Trim();

				var token = await userManager.GenerateChangeEmailTokenAsync(model, email);
				var changeEmailResult = await userManager.ChangeEmailAsync(model, email, token);
				if (!changeEmailResult.Succeeded)
				{
					return ErrorResult
					(
						status: StatusCodes.Status400BadRequest,
						title: $"Failed to update user",
						detail: $"There's an error in updating the email address of user {id}."
					);
				}

				var updateUserResult = await userManager.UpdateAsync(model);
				if (!updateUserResult.Succeeded)
				{
					return ErrorResult
					(
						status: StatusCodes.Status400BadRequest,
						title: $"Failed to update user",
						detail: $"There's an error in updating the user profile of user {id}."
					);
				}

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}


		// POST: Users/Delete/5
		// [ValidateAntiForgeryToken]
		public ActionResult Delete(string id)
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

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
	[Authorize(Roles = "Administrator")]
	public class UsersController : BaseController
	{
		//private readonly IUserRoleRepository userRoleRepository;

		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<ApplicationRole> roleManager;
		private readonly IEmailService emailService;

		public UsersController
		(
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager,
			IEmailService emailService,
			ILogger<UsersController> logger,
			IUserRepository userRepository,
			IActivityLogRepository activityLogRepository
		) : base
		(
			userRepository,
			logger,
			activityLogRepository
		)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.emailService = emailService;
		}

		// GET: Users
		public ActionResult Index()
		{
			try
			{
				var list = userRepository.GetAll().ToList();

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
				//var model = userRepository.Get(id);
				var model = userRepository.GetAll().SingleOrDefault(x => x.Id == id);

				if (model == null)
					return NotFound();

				var viewModel = new UserViewModel(model)
				{
					Roles = model.Roles
						.Select(role => new UserRolesViewModel
						{
							Id = role.Role.Id,
							Name = role.Role.Name,
							Selected = true
						}).ToList()
				};

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
			try
			{
				var allRoles = Enumeration<string>.GetAll<UserRole>();

				var viewModel = new UserWithPasswordViewModel
				{
					Roles = allRoles
						.Select(model => new UserRolesViewModel
						{
							Id = model.Id,
							Name = model.Name
						}).ToList()
				};

				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind("Password, Email, FirstName, LastName, Roles")] UserWithPasswordViewModel viewModel)
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

				var addRoles = viewModel.Roles.Where(x => x.Selected).Select(role => role.Name).ToArray();
				await userManager.AddToRolesAsync(model, addRoles);

				//var recipient = new MailAddress(email, $"{model.FirstName} {model.LastName}");
				//await emailService.SendPasswordAsync(recipient, tempPassword);

				LogUserActivity("Created a User", $"Created a new User - {model.UserName}");

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
				//var model = this.userRepository.Get(id);
				var model = userRepository.GetAll().SingleOrDefault(x => x.Id == id);
				if (model == null)
					return NotFound();

				var allRoles = Enumeration<string>.GetAll<UserRole>();
				var modelRoleIds = model.Roles.Select(role => role.UserRoleId).ToArray();

				var viewModel = new UserViewModel(model)
				{
					Roles = allRoles
						.Select(role => new UserRolesViewModel
						{
							Id = role.Id,
							Name = role.Name,
							Selected = modelRoleIds.Contains(role.Id)
						}).ToList()
				};

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
		public async Task<ActionResult> Edit(string id, [Bind("Email, FirstName, LastName, Roles")] UserViewModel viewModel)
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

				var removeRoles = await userManager.GetRolesAsync(model);
				await userManager.RemoveFromRolesAsync(model, removeRoles);

				var addRoles = viewModel.Roles.Where(x => x.Selected).Select(role => role.Name).ToArray();
				await userManager.AddToRolesAsync(model, addRoles);

				LogUserActivity("Updated a User", $"User '{model.UserName}' [{model.Id}] has been updated.");

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

				LogUserActivity("Deleted a User", $"User '{model.UserName}' [{model.Id}] has been deleted.");

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

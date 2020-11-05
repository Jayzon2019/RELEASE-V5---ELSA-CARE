using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Core.Services;

using InLife.Store.Cms.ViewModels;
using InLife.Store.Cms.Data;
using InLife.Store.Cms.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;


namespace InLife.Store.Cms.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IEmailService emailService;

		private readonly IKeyMetricRepository keyMetricRepository;
		private readonly IActivityLogRepository activityLogRepository;

		public HomeController
		(
			UserManager<ApplicationUser> userManager,
			IEmailService emailService,
			ILogger<FaqCategoriesController> logger,
			IUserRepository userRepository,
			IKeyMetricRepository keyMetricRepository,
			IActivityLogRepository activityLogRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.userManager = userManager;
			this.emailService = emailService;

			this.keyMetricRepository = keyMetricRepository;
			this.activityLogRepository = activityLogRepository;
		}

		public IActionResult Index()
		{
			try
			{
				//var viewModelList = keyMetricRepository
				//	.GetAll()
				//	.Select(model => new KeyMetricViewModel(model))
				//	.ToList();

				//if (viewModelList == null)
				//	return NotFound();

				//return View(viewModelList);

				var viewModel = new KeyMetricViewModel
				{
					Id = 0,
					PageName = "",
					PageViews = 0,
					Sessions = "",
					UserCount = 0,
					ActivityLogs = new List<ActivityLogViewModel>()
				};
				return View(viewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}

			//var log = "";
			//try
			//{
			//	var homeData = HS.GetHomeData(ref log);
			//	return View(homeData);
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	return NotFound();
			//}
		}
		[AllowAnonymous]
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
			//return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public IActionResult Logout()
		{
			// HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//return RedirectToAction("Login", "Login");
			return new SignOutResult(new[]
			{
				CookieAuthenticationDefaults.AuthenticationScheme,
				OpenIdConnectDefaults.AuthenticationScheme
			});
		}

		public IActionResult EditProfile()
		{
			try
			{
				var currentUserViewModel = new UserViewModel(this.CurrentUser());

				return View(currentUserViewModel);
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditProfile([Bind("Email, FirstName, LastName")] UserViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
				var id = CurrentUser().Id;
				var model = await userManager.FindByIdAsync(id);

				if (model == null)
					return NotFound();

				var email = viewModel.Email.ToLower().Trim();
				model.UserName = email;
				model.Email = email;
				model.FirstName = viewModel.FirstName.Trim();
				model.LastName = viewModel.LastName.Trim();

				var token = await userManager.GenerateChangeEmailTokenAsync(model, email);
				await userManager.ChangeEmailAsync(model, email, token);

				await userManager.UpdateAsync(model);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}

			//var log = "";
			//ModelState.Remove("Password");
			//ModelState.Remove("password");
			//ModelState.Remove("conPassword");
			//if (ModelState.IsValid)
			//{
			//	try
			//	{
			//		siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
			//		US.EditUser(ref log, usersViewModel);
			//		usersViewModel = US.GetUserById(ref log, usersViewModel.intUserId);
			//		HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//		var identity = new ClaimsIdentity(new[] {
			//		new Claim(ClaimTypes.Email, usersViewModel.Email),
			//		new Claim(ClaimTypes.Name, usersViewModel.FirstName + " " + usersViewModel.LastName),
			//		new Claim(ClaimTypes.Role, usersViewModel.strUserRole),
			//		new Claim(ClaimTypes.Surname, usersViewModel.UserImg),
			//		new Claim(ClaimTypes.Sid, usersViewModel.intUserId.ToString())
			//	}, CookieAuthenticationDefaults.AuthenticationScheme);
			//		var principal = new ClaimsPrincipal(identity);
			//		HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			//	}
			//	catch (Exception ex)
			//	{
			//		string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//		var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//		lR.SaveExceptionLogs(exLog, ex, methodName);
			//		ViewBag.error = Comman.SomethingWntWrong;
			//		return View();
			//	}
			//	return RedirectToAction(nameof(Index));
			//}
			//else
			//{
			//	usersViewModel = US.GetUserById(ref log, id);
			//	var genderList = US.GetGenders(ref log);
			//	ViewBag.genList = genderList;
			//}
			//return View(usersViewModel);
		}

		public IActionResult ChangePassword()
		{
			try
			{
				//var currentUserViewModel = new UserPasswordViewModel(this.CurrentUser());

				return View();
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword([Bind("OldPassword", "NewPassword1", "NewPassword2")] UserPasswordViewModel viewModel)
		{
			try
			{
				if (viewModel.NewPassword1 != viewModel.NewPassword2)
				{
					ViewBag.error = "Passwords did not match. Please check passwords and try again.";
					return View();
				}

				if (!ModelState.IsValid)
					return View(viewModel);

				var id = CurrentUser().Id;
				var model = await userManager.FindByIdAsync(id);

				if (model == null)
					return NotFound();

				var result = await userManager.ChangePasswordAsync(model, viewModel.OldPassword, viewModel.NewPassword1);

				return new SignOutResult(new[]
				{
					CookieAuthenticationDefaults.AuthenticationScheme,
					OpenIdConnectDefaults.AuthenticationScheme
				});
				//return RedirectToAction(nameof(Index));
			}
			catch (Exception e)
			{
				return GenericServerErrorResult(e);
			}
			//var log = "";
			//try
			//{
			//	if (password == conPassword)
			//	{
			//		bool isChanged = US.ChangePassword(ref log, password);
			//		if (isChanged)
			//		{
			//			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//			return RedirectToAction("Login", "Login");
			//		}
			//	}
			//	else
			//	{
			//		ViewBag.error = "Password did not match. Please check password and try again";
			//	}
			//	return View();
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	ViewBag.error = Comman.SomethingWntWrong;
			//	return View();
			//}
		}

		[AllowAnonymous]
		public IActionResult ActivateAccount(int uid, string token)
		{
			return View();
			//var log = "";
			//try
			//{
			//	var user = US.VerifyToken(ref log, uid, token);
			//	if (user != null)
			//	{
			//		return View(user);
			//	}
			//	else
			//	{
			//		return null;
			//	}
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	ViewBag.error = Comman.SomethingWntWrong;
			//	return null;
			//}
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> SetPassword(int intUserId, string password, string conPassword)
		{
			await Task.Delay(0);
			return View();
			//var log = "";
			//try
			//{
			//	if (password == conPassword)
			//	{
			//		bool isChanged = US.SetPassword(ref log, intUserId, password);
			//		if (isChanged)
			//		{
			//			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//			return RedirectToAction("Login", "Login");
			//		}
			//	}
			//	else
			//	{
			//		ViewBag.error = "Password did not match. Please check password and try again";
			//	}
			//	return View();
			//}
			//catch (Exception ex)
			//{
			//	string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
			//	var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
			//	lR.SaveExceptionLogs(exLog, ex, methodName);
			//	ViewBag.error = Comman.SomethingWntWrong;
			//	return View();
			//}
		}
	}
}

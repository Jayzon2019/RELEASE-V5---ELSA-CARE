using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;
using InLife.Store.Cms.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace InLife.Store.Cms.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly IKeyMetricRepository keyMetricRepository;
		private readonly IActivityLogRepository activityLogRepository;

		public HomeController
		(
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

			//var log = "";
			//try
			//{
			//	int id = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
			//	if (id == 0)
			//	{
			//		ViewBag.error = "User not found!";
			//		return NotFound();
			//	}
			//	var genderList = US.GetGenders(ref log);
			//	ViewBag.genList = genderList;
			//	var usersViewModel = US.GetUserById(ref log, id);
			//	if (usersViewModel == null)
			//	{
			//		ViewBag.error = "User not found!";
			//		return NotFound();
			//	}
			//	return View(usersViewModel);
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
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(int id, [Bind("UserName, FirstName, LastName")] UserViewModel userViewModel)
		{
			return View(userViewModel);

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
			return View();
			//var log = "";
			//try
			//{
			//	int id = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
			//	if (id == 0)
			//	{
			//		ViewBag.error = "User not Found!";
			//		return NotFound();
			//	}
			//	var usersViewModel = US.GetUserById(ref log, id);
			//	if (usersViewModel == null)
			//	{
			//		ViewBag.error = "User not Found!";
			//		return NotFound();
			//	}
			//	return View(usersViewModel);
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
		public async Task<IActionResult> ChangePassword(string password, string conPassword)
		{
			await Task.Delay(0);
			return View();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InLifeCMS.Services;
using InLifeCMS.ViewModel;
using System.Diagnostics;
using InLifeCMS.Helpers;

namespace InLifeCMS.Controllers
{
    public class LoginController : Controller
    {
        LoginService loginService = new LoginService();

        // GET: Login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, bool rememberMe)
        {
            string log = "";
            try
            {
                var principal = loginService.AuthticateUser(ref log, email, password, rememberMe);
                if (principal?.Identity.IsAuthenticated == true)
                {
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Invalid Email Or Password. Please verify your details!!!";
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
            catch (Exception ex)
            {
                var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Comman.ExceptionLogBulder(log , methodName , ex);
                ViewBag.error = Comman.SomethingWntWrong;
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}
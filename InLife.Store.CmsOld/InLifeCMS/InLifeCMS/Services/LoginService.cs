using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InLifeCMS.Helpers;
using InLifeCMS.Repos;

namespace InLifeCMS.Services
{
    public class LoginService
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        LogsRepo lp = new LogsRepo();
        LoginRepo loginRepo = new LoginRepo();
        public ClaimsPrincipal AuthticateUser(ref string log, string email, string password, bool RememberMe)
        {
            try
            {
                if (RememberMe)
                {
                    var cookieOptions = new CookieOptions
                    {
                        IsEssential = true,
                        Expires = DateTime.Now.AddDays(7)
                    };
                    httpContextAccessor.HttpContext.Response.Cookies.Append("email", email, cookieOptions);
                    httpContextAccessor.HttpContext.Response.Cookies.Append("password", password, cookieOptions);
                }
                else
                {
                    httpContextAccessor.HttpContext.Response.Cookies.Delete("email");
                    httpContextAccessor.HttpContext.Response.Cookies.Delete("password");
                }
                var principle = loginRepo.AuthticateUser(ref log, email, password);
                return principle;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lp.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }
    }
}

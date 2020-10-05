using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InLifeCMS.Repos
{
    public class LoginRepo
    {
        LogsService ls = new LogsService();
        LogsRepo lp = new LogsRepo();
        //dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        public ClaimsPrincipal AuthticateUser(ref string log, string email, string password)
        {
            try
            {
                var pass = Comman.Encrypt(password);
                var user = db.TblUsers.Where(x => x.IsActive == true && x.Email == email && x.Password == pass).FirstOrDefault();
                if (user != null)
                {
                    var img = "";
                    if (user.UserImg != null && user.UserImg != "")
                    {
                        img = user.UserImg;
                    }
                    else
                    {
                        img = "images/UserImgs/avatar.jpg";
                    }
                    var role = db.TblUserRoles.Where(x => x.UserRoleId == user.UserRoleId).FirstOrDefault();
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Role, role.UserRole),
                    new Claim(ClaimTypes.Surname, img),
                    new Claim(ClaimTypes.Sid, user.UserId.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var activityDes = Comman.ActivityloginDescription(user.FirstName + user.LastName, user.UserId , Comman.ActivityActions.Logged_In.ToString());
                    ls.SaveLogininActivityLogs(user.FirstName + user.LastName, user.UserId, Comman.ActivityActions.Logged_In.ToString(), activityDes);
                    return principal;
                }
                else
                {
                    return null;
                }
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

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
		private readonly IUserRepository userRepository;

		public UsersController
		(
			ILogger<UsersController> logger,
			IUserRepository userRepository
		) : base
		(
			logger
		)
		{
			this.userRepository = userRepository;
		}


		private IWebHostEnvironment _webHostEnvironment;
        public UsersController(IWebHostEnvironment hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
        }
        UsersService US = new UsersService();
        LogsRepo lR = new LogsRepo();

        // GET: Users
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var userList = US.GetUsers(ref log);
                if (userList != null)
                {
                    return View(userList);
                }
                else
                {
                    ViewBag.error = "No record found";
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return NotFound();
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var usersViewModel = US.GetUserById(ref log, id);
                if (usersViewModel == null)
                {
                    ViewBag.error = "User not found";
                    return NotFound();
                }

                return View(usersViewModel);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return NotFound();
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var log = "";
            try
            {
                var genderList = US.GetGenders(ref log);
                ViewBag.genList = genderList;
                var roleList = US.GetRoles(ref log);
                ViewBag.roleList = roleList;

                return View();

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return NotFound();
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("dteDob,intGenderId,strFirstName,strLastName,intUserRoleId,strEmail,strPassword,strUserImg,strPhone,strGender,strUserRole")] UsersViewModel usersViewModel)
        {
            var log = "";
            try
            {
                ModelState.Remove("password");
                ModelState.Remove("conPassword");
                if (ModelState.IsValid)
                {
                    siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
                   var isSaved = US.SaveUser(ref log, usersViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var genderList = US.GetGenders(ref log);
                    ViewBag.genList = genderList;
                    var roleList = US.GetRoles(ref log);
                    ViewBag.roleList = roleList;
                }
                return View(usersViewModel);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return View();
            }

        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var genderList = US.GetGenders(ref log);
                ViewBag.genList = genderList;
                var roleList = US.GetRoles(ref log);
                ViewBag.roleList = roleList;
                var usersViewModel = US.GetUserById(ref log, id);
                if (usersViewModel == null)
                {
                    return NotFound();
                }
                return View(usersViewModel);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return NotFound();
            }


        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("intUserId,dteDob,intGenderId,strFirstName,strLastName,intUserRoleId,strEmail,strPassword,strUserImg,strPhone,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strRestCode,steRestDate,strActivationCode,dteActivationDate")] UserViewModel usersViewModel)
        {
            var log = "";
          //  ModelState.Remove("strPassword");
            ModelState.Remove("password");
            ModelState.Remove("conPassword");
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != usersViewModel.intUserId)
                    {
                        return NotFound();
                    }
                    siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
                    US.EditUser(ref log, usersViewModel);
                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                    lR.SaveExceptionLogs(exLog, ex, methodName);
                    ViewBag.error = Comman.SomethingWntWrong;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var genderList = US.GetGenders(ref log);
                ViewBag.genList = genderList;
                var roleList = US.GetRoles(ref log);
                ViewBag.roleList = roleList;
                usersViewModel = US.GetUserById(ref log, id);
                if (usersViewModel == null)
                {
                    return NotFound();
                }
                return View(usersViewModel);

            }
        }


        // POST: Users/Delete/5
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var log = "";
            try
            {

                US.Deactivate_DeleteUser(ref log, Convert.ToInt32(id), true);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Deactive")]
       // [ValidateAntiForgeryToken]
        public ActionResult Deactive(int id)
        {
            var log = "";
            try
            {
                US.Deactivate_DeleteUser(ref log, id, false);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return RedirectToAction(nameof(Index));
            }
        }

    }
}

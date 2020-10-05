using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InLifeCMS.Models;
using InLifeCMS.ViewModel;
using InLifeCMS.Helpers;
using Microsoft.AspNetCore.Hosting;
using InLifeCMS.Services;
using InLifeCMS.Repos;
using Microsoft.AspNetCore.Authorization;

namespace InLifeCMS.Controllers
{
    [Authorize]
    public class PrimeCareController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public PrimeCareController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        PrimeCareService PCS = new PrimeCareService();
        LogsRepo lR = new LogsRepo();


        // GET: PrimeCare
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var PrimeCareList = PCS.GetPrimeCareFiles(ref log);
                if (PrimeCareList != null)
                {
                    return View(PrimeCareList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: PrimeCare/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var PrimeCareFile = PCS.GetPrimeCareFileById(ref log, id);
                if (PrimeCareFile == null)
                {
                    return NotFound();
                }

                return View(PrimeCareFile);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
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
        public ActionResult Create([Bind("intPrimeCareId,strPrimeCareFile,strPrimeCareFileName,strPrimeCareFileDescription,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived")] PrimeCareViewModel primeCareViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    var isSaved = PCS.SavePrimeCareFile(ref log, primeCareViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(primeCareViewModel);
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

        // GET: PrimeCare/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var primeCareVM = PCS.GetPrimeCareFileById(ref log, id);
                if (primeCareVM == null)
                {
                    return NotFound();
                }
                return View(primeCareVM);
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

        // POST: PrimeCare/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit(int id, [Bind("intPrimeCareId,strPrimeCareFile,strPrimeCareFileName,strPrimeCareFileDescription,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived")] PrimeCareViewModel primeCareViewModel)
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != primeCareViewModel.intPrimeCareId)
                    {
                        return NotFound();
                    }
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    PCS.EditPrimeCareFile(ref log, primeCareViewModel);
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
            return View(primeCareViewModel);
        }


        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                PCS.Deactivate_DeletePrimeCareFile(ref log, id, true);
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
       // [HttpPost, ActionName("Deactive")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeactiveHero(int id)
        {
            var log = "";
            try
            {
                PCS.Deactivate_DeletePrimeCareFile(ref log, id, false);
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

        [HttpGet]
        public ActionResult GetPdf(string path)
        {
            var log = "";
            try
            {
                string filePath = path;
                var fileName = path.Split('\\').Last(); 
                Response.Headers.Add("Content-Disposition", "inline; " + fileName + " ");
                return File(filePath, "application/pdf");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                ViewBag.error = Comman.SomethingWntWrong;
                return RedirectToAction(nameof(Index));
            }}
    }
}

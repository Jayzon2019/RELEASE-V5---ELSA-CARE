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
    [Authorize]
    public class PrimeCareController : BaseController
    {
		private readonly IPrimeCareRepository primeCareRepository;

		public PrimeCareController
		(
			ILogger<PrimeCareController> logger,
			IUserRepository userRepository,
			IPrimeCareRepository primeCareRepository
		) : base
		(
			userRepository,
			logger
		)
		{
			this.primeCareRepository = primeCareRepository;
		}



		private IWebHostEnvironment _webHostEnvironment;
        public PrimeCareController(IWebHostEnvironment hostingEnvironment)
        {
            _webHostEnvironment = hostingEnvironment;
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
                    siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
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
                    siteOptions.DirectoryPath = _webHostEnvironment.WebRootPath;
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

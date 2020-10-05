using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InLifeCMS.Models;
using InLifeCMS.ViewModel;
using InLifeCMS.Services;
using InLifeCMS.Repos;
using Microsoft.AspNetCore.Hosting;
using InLifeCMS.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace InLifeCMS.Controllers
{
    [Authorize]
    public class PrimeHeroController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public PrimeHeroController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        PrimeHeroService PHS = new PrimeHeroService();
        LogsRepo lR = new LogsRepo();
        // GET: PrimeHero
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var primeHeroList = PHS.GetPrimeHeroSliders(ref log);
                if (primeHeroList != null)
                {
                    return View(primeHeroList);
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

        // GET: PrimeHero/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var PrimeHeroSlider = PHS.GetPrimeHeroSliderById(ref log, id);
                if (PrimeHeroSlider == null)
                {
                    return NotFound();
                }

                return View(PrimeHeroSlider);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: PrimeHero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrimeHero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("intPrimeHeroId,strPrimeHeroBg,strPrimeHeroTitle,strPrimeHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] PrimeHeroViewModel primeHeroViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    var isSaved = PHS.SavePrimeHeroSlider(ref log, primeHeroViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(primeHeroViewModel);
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

        // GET: PrimeHero/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var primeHeroVM = PHS.GetPrimeHeroSliderById(ref log, id);
                if (primeHeroVM == null)
                {
                    return NotFound();
                }
                return View(primeHeroVM);
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

        // POST: PrimeHero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("intPrimeHeroId,strPrimeHeroBg,strPrimeHeroTitle,strPrimeHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] PrimeHeroViewModel primeHeroViewModel)
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != primeHeroViewModel.intPrimeHeroId)
                    {
                        return NotFound();
                    }
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    PHS.EditPrimeHeroSlider(ref log, primeHeroViewModel);
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
            return View(primeHeroViewModel);


        }

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                PHS.Deactivate_DeletePrimeHeroSlider(ref log, id, true);
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
                PHS.Deactivate_DeletePrimeHeroSlider(ref log, id, false);
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

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
    public class HeroController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public HeroController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        HeroService HS = new HeroService();
        LogsRepo lR = new LogsRepo();


        // GET: Hero
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var heroList = HS.GetHeroSliders(ref log);
                if (heroList != null)
                {
                    return View(heroList);
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

        // GET: Hero/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var HeroSlider = HS.GetHeroSliderById(ref log, id);
                if (HeroSlider == null)
                {
                    return NotFound();
                }

                return View(HeroSlider);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: Hero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("intHeroId,strHeroBg,strHeroTitle,strHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] HeroViewModel heroViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    var isSaved = HS.SaveHeroSlider(ref log, heroViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(heroViewModel);
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

        // GET: Hero/Edit/5
        public IActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var heroVM = HS.GetHeroSliderById(ref log, id);
                if (heroVM == null)
                {
                    return NotFound();
                }
                return View(heroVM);
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

        // POST: Hero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("intHeroId,strHeroBg,strHeroTitle,strHeroBtnTxt,strBtnTxtLink,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,strHeading,strSubHeading,strHeadingColor,strSubHeadingColor,strContentPostion")] HeroViewModel heroViewModel)
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != heroViewModel.intHeroId)
                    {
                        return View();
                    }
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    HS.EditHeroSlider(ref log, heroViewModel);
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
            return View(heroViewModel);

        }


        // POST: Users/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                HS.Deactivate_DeleteHeroSlider(ref log, id, true);
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
                HS.Deactivate_DeleteHeroSlider(ref log, id, false);
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

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
using InLifeCMS.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace InLifeCMS.Controllers
{
    [Authorize]
    public class FaqController : Controller
    {
        FaqService FS = new FaqService();
        FaqCategoriesService FCS = new FaqCategoriesService();
        LogsRepo lR = new LogsRepo();
        FaqRepo FR = new FaqRepo();
        // GET: Faq
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var faqCatList = FS.GetFaqList(ref log);
                if (faqCatList != null)
                {
                    return View(faqCatList);
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

        // GET: Faq/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var faq = FS.GetFaqById(ref log, id);
                if (faq == null)
                {
                    return NotFound();
                }

                return View(faq);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: Faq/Create
        public IActionResult Create()
        {
            var log = "";
            try
            {
                var faqCatDrp = FCS.GetFaqCatList(ref log);
                ViewBag.faqCatDrp = faqCatDrp;
                return View();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // POST: Faq/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("intFaqId,intFaqCatId,strFaqQuestion,strFaqAnswer,strCreatedByUser,strUpdatedByUser,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,intSortNum")] FaqViewModel faqViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var isSaved = FS.SaveFaq(ref log, faqViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var faqCatDrp = FCS.GetFaqCatList(ref log);
                        ViewBag.faqCatDrp = faqCatDrp;
                        /// ModelState.AddModelError("intSortNum", "Sort Number already exists Please assign other number");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    var faqCatDrp = FCS.GetFaqCatList(ref log);
                    ViewBag.faqCatDrp = faqCatDrp;

                }
                return View(faqViewModel);
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

        // GET: Faq/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var faqCatDrp = FCS.GetFaqCatList(ref log);
                ViewBag.faqCatDrp = faqCatDrp;
                var faqVM = FS.GetFaqById(ref log, id);
                if (faqVM == null)
                {
                    return NotFound();
                }
                return View(faqVM);
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

        // POST: Faq/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("intFaqId,intFaqCatId,strFaqQuestion,strFaqAnswer,strCreatedByUser,strUpdatedByUser,dteCreatedDate,intCreatedBy,dteUpdatedDate,intUpdatedBy,blnIsActive,blnIsArchived,intSortNum")] FaqViewModel faqViewModel)
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != faqViewModel.intFaqId)
                    {
                        return NotFound();
                    }
                    var updateFaq = FS.EditFaq(ref log, faqViewModel);
                    if (updateFaq == "Updated")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var faqCatDrp = FCS.GetFaqCatList(ref log);
                        ViewBag.faqCatDrp = faqCatDrp;
                        var faqVM = FS.GetFaqById(ref log, id);
                        ModelState.AddModelError("intSortNum", Comman.SomethingWntWrong);
                        return View(faqVM);
                    }
                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                    lR.SaveExceptionLogs(exLog, ex, methodName);
                    ViewBag.error = Comman.SomethingWntWrong;
                    var faqCatDrp = FCS.GetFaqCatList(ref log);
                    ViewBag.faqCatDrp = faqCatDrp;
                    var faqVM = FS.GetFaqById(ref log, id);
                    return View(faqVM);
                }
            }
            else
            {
                var faqCatDrp = FCS.GetFaqCatList(ref log);
                ViewBag.faqCatDrp = faqCatDrp;
            }
            return View(faqViewModel);

        }

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                FS.Deactivate_DeleteFaq(ref log, id, true);
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
        public ActionResult DeactiveHero(int id)
        {
            var log = "";
            try
            {
                FS.Deactivate_DeleteFaq(ref log, id, false);
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

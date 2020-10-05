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
using InLifeCMS.Services;
using InLifeCMS.Repos;
using Microsoft.AspNetCore.Authorization;

namespace InLifeCMS.Controllers
{
    [Authorize]
    public class FooterLinksController : Controller
    {
        FooterLinksService FLS = new FooterLinksService();
        LogsRepo lR = new LogsRepo();
        FaqRepo FR = new FaqRepo();
        // GET: FooterLinks
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var footerLinksList = FLS.GetFooterLinksList(ref log);
                if (footerLinksList != null)
                {
                    return View(footerLinksList);
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

        // GET: FooterLinks/Details/5
        public ActionResult Details()
        {
            var log = "";
            try
            {

                var footerLink = FLS.GetFooterLink(ref log);
                if (footerLink == null)
                {
                    return NotFound();
                }

                return View(footerLink);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: FooterLinks/Create
        public IActionResult Create()
        {
            var log = "";
            try
            {

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

        // POST: FooterLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("intFooterLinkId,strLogoUrl,strMainSiteUrl,strInsCommissionUrl,strCusCharterUrl,strTermsConditionUrl,strPrivacyPolicyUrl,strContactUsUrl,strFbUrl,strTweeterUrl,strInstaUrl,strYouTubeUrl,strMainSiteTxt , strInsCommissionTxt, strCusCharterTxt, strTermsConditionTxt,  strPrivacyPolicyTxt,   strContactUsTxt ")] FooterLinksViewModel footerLinksViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var isSaved = FLS.SaveFooterLink(ref log, footerLinksViewModel);
                    if (isSaved == "Saved")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.error = Comman.SomethingWntWrong;
                    }
                }
                return View(footerLinksViewModel);
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

        // GET: FooterLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var footerVM = FLS.GetFooterLinkById(ref log, id);
                if (footerVM == null)
                {
                    return NotFound();
                }
                return View(footerVM);
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

        // POST: FooterLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("intFooterLinkId,strLogoUrl,strMainSiteUrl,strInsCommissionUrl,strCusCharterUrl,strTermsConditionUrl,strPrivacyPolicyUrl,strContactUsUrl,strFbUrl,strTweeterUrl,strInstaUrl,strYouTubeUrl,strMainSiteTxt , strInsCommissionTxt, strCusCharterTxt, strTermsConditionTxt,  strPrivacyPolicyTxt,   strContactUsTxt ")] FooterLinksViewModel footerLinksViewModel) 
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != footerLinksViewModel.intFooterLinkId)
                    {
                        return NotFound();
                    }
                    var updateFooterLink = FLS.EditFooterLink(ref log, footerLinksViewModel);
                    if (updateFooterLink == "Updated")
                    {
                        return RedirectToAction(nameof(Details));
                    }
                    else
                    {
                        ViewBag.error = Comman.SomethingWntWrong;
                        var footerVM = FLS.GetFooterLinkById(ref log, id);
                        return View(footerVM);
                    }
                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                    lR.SaveExceptionLogs(exLog, ex, methodName);
                    ViewBag.error = Comman.SomethingWntWrong;
                    var footerVM = FLS.GetFooterLinkById(ref log, id);
                    return View(footerVM);
                }
            }
            return View(footerLinksViewModel);

        }

        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                FLS.Deactivate_DeleteFooterLink(ref log, id);
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

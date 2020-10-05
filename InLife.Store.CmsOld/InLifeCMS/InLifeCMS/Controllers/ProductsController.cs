using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InLifeCMS.Models;
using InLifeCMS.ViewModels;
using InLifeCMS.Helpers;
using Microsoft.AspNetCore.Hosting;
using InLifeCMS.Services;
using InLifeCMS.Repos;
using Microsoft.AspNetCore.Authorization;

namespace InLifeCMS.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        public ProductsController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        ProductsService PS = new ProductsService();
        LogsRepo lR = new LogsRepo();

        // GET: Products
        public ActionResult Index()
        {
            var log = "";
            try
            {
                var proList = PS.GetProductsList(ref log);
                if (proList != null)
                {
                    return View(proList);
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

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Pro = PS.GetProductById(ref log, id);
                if (Pro == null)
                {
                    return NotFound();
                }

                return View(Pro);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("intProductId,strProductImg,strProductName,strProductPrice,strProductCode,strShortDescription,strPriceWithOffer,intSortNum")] ProductsViewModel productsViewModel)
        {
            var log = "";
            try
            {
                if (ModelState.IsValid)
                {
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    var isSaved = PS.SaveProduct(ref log, productsViewModel);
                    if (isSaved != "Saved")
                    {
                        ViewBag.error = isSaved;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(productsViewModel);
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

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            var log = "";
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var proVM = PS.GetProductById(ref log, id);
                if (proVM == null)
                {
                    return NotFound();
                }
                return View(proVM);
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("intProductId,strProductImg,strProductName,strProductPrice,strProductCode,strShortDescription,strPriceWithOffer,intSortNum")] ProductsViewModel productsViewModel)
        {
            var log = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != productsViewModel.intProductId)
                    {
                        return NotFound();
                    }
                    siteOptions.DirectoryPath = _hostingEnvironment.WebRootPath;
                    PS.EditProducts(ref log, productsViewModel);
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
            return View(productsViewModel);
        }


        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var log = "";
            try
            {
                PS.Deactivate_DeleteProduct(ref log, id, true);
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
        [HttpPost, ActionName("Deactive")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactiveHero(int id)
        {
            var log = "";
            try
            {
                PS.Deactivate_DeleteProduct(ref log, id, false);
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

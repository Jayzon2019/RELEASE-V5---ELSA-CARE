using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InLife.Store.Api.Services;
using InLife.Store.Api.Repos;
using InLife.Store.Api.Helpers;
using System.Net.Mime;

namespace InLife.Store.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class HomeController : Controller
    {
        LogsRepo lR = new LogsRepo();
        HomeService HS = new HomeService();

        [HttpGet]
        [Route("GetHeroSliders")]
        public IActionResult GetHeroSliders()
        {
            var log = "";
            try
            {
                var result = HS.GetHeroSliders(ref log);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetProductsList")]
        public IActionResult GetProductsList()
        {
            var log = "";
            try
            {
                var result = HS.GetProductsList(ref log);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetProductsDetailList")]
        public IActionResult GetProductsDetailList()
        {
            var log = "";
            try
            {
                var result = HS.GetProductsDetailList(ref log);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }


        [HttpGet]
        [Route("GetPageId_SetPageView")]
        public IActionResult GetPageId_SetPageView(string page)
        {
            var log = "";
            try
            {
                var result = HS.GetPageId_SetPageView(ref log, page);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("SetUserLeaveTime")]
        public IActionResult SetUserLeaveTime(int id)
        {
            var log = "";
            try
            {
                HS.SetUserLeaveTime(ref log, id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetFiles")]
        public IActionResult GetFiles()
        {
            var log = "";
            try
            {
                var obj = HS.GetFiles(ref log);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return NotFound();
            }
        }

    }
}
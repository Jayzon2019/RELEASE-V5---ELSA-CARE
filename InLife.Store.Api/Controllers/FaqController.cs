using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using InLife.Store.Api.Helpers;
using InLife.Store.Api.Repos;
using InLife.Store.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InLife.Store.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class FaqController : Controller
    {
        LogsRepo lR = new LogsRepo();
        FaqService FS = new FaqService();

        [HttpGet]
        [Route("GetFaqCatList")]
        public IActionResult GetFaqCatList()
        {
            var log = "";
            try
            {
                var result = FS.GetFaqCatList(ref log);
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
        [Route("GetFaqListByCatId")]
        public IActionResult GetFaqListByCatId(int id)
        {
            var log = "";
            try
            {
                var result = FS.GetFaqListByCatId(id , ref log);
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
        [Route("GetFaqList")]
        public IActionResult GetFaqList()
        {
            var log = "";
            try
            {
                var result = FS.GetFaqList(ref log);
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

    }
}
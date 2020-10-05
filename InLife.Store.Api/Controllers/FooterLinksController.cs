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
    public class FooterLinksController : Controller
    {
        LogsRepo lR = new LogsRepo();
        FooterLinksService FLS = new FooterLinksService();


        [HttpGet]
        [Route("GetFooterLinks")]
        public IActionResult GetFooterLinks()
        {
            var log = "";
            try
            {
                var result = FLS.GetFooterLinks(ref log);
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
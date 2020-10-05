using InLifeCMS.Repos;
using InLifeCMS.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InLifeCMS.Services
{
    public class LogsService
    {
        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        LogsRepo lp = new LogsRepo();
        public void SaveActivityLogs(string actionPerformed, string activityDes)
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            ActivityLogsViewModel AL = new ActivityLogsViewModel
            {
                intActivityById = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                strActivityBy = httpContextAccessor.HttpContext.User.Identity.Name,
                dteActivityDate = DateTime.Now,
                strIpAddress = myIP,
                strActionPerfomed = actionPerformed,
                strActivityDescription = activityDes
            };
            lp.saveActivityLogs(AL);
        }
        public void SaveLogininActivityLogs(string userName , int userId ,string actionPerformed, string activityDes)
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            ActivityLogsViewModel AL = new ActivityLogsViewModel
            {
                intActivityById = userId,
                strActivityBy = userName,
                dteActivityDate = DateTime.Now,
                strIpAddress = myIP,
                strActionPerfomed = actionPerformed,
                strActivityDescription = activityDes
            };
            lp.saveActivityLogs(AL);
        }

    }
}

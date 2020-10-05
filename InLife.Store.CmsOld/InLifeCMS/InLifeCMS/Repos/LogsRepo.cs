using InLifeCMS.ViewModel;
using InLifeCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using InLifeCMS.Helpers;

namespace InLifeCMS.Repos
{
    public class LogsRepo
    {

        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        public void SaveExceptionLogs(string log, Exception ex, string methodName)
        {
            var innerEx = "";
            if(ex.InnerException!= null)
            {
                innerEx = ex.InnerException.Message;
            }
            TblExceptionLogs tblExLogs = new TblExceptionLogs
            {
                ExMsg = log,
                ExSource = ex.StackTrace,
                ExUrl = methodName,
                ExDate = DateTime.Now,
                ExInner = innerEx
            };
            db.TblExceptionLogs.Add(tblExLogs);
            db.SaveChanges();
        }

        public void saveActivityLogs(ActivityLogsViewModel ALVM)
        {
            try
            {
                TblActivityLogs AL = new TblActivityLogs
                {
                    ActivityById = ALVM.intActivityById,
                    ActivityBy = ALVM.strActivityBy,
                    ActivityDate = ALVM.dteActivityDate,
                    IpAddress = ALVM.strIpAddress,
                    ActionPerfomed = ALVM.strActionPerfomed,
                    ActivityDescription = ALVM.strActivityDescription
                };
                db.TblActivityLogs.Add(AL);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var log = Comman.ExceptionLogBulder("", methodName, ex);
                SaveExceptionLogs(log, ex, methodName);
            }
        }
    }
}

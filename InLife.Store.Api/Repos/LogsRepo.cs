using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using InLife.Store.Api.Models;

namespace InLife.Store.Api.Repos
{
    public class LogsRepo
    {

          InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
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

    }
}

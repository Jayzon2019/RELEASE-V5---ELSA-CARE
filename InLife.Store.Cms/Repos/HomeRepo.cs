using InLifeCMS.Helpers;
using InLifeCMS.ViewModel;
using InLifeCMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Repos
{
    public class HomeRepo
    {
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        public List<TblActivityLogs> GetActivityLogs(ref string log)
        {
            try
            {
                var activityLogs = db.TblActivityLogs.OrderByDescending(x => x.ActivityDate).ToList();
                return activityLogs;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public List<TblKeyMetrics> GetKeyMetrics(ref string log)
        {
            try
            {
                var keyMetrics = db.TblKeyMetrics.Where(x=>x.KeyMetricsId > 0).ToList();
                return keyMetrics;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

       public int GetUserCount(ref string log)
        {

            try
            {
                return db.TblUsers.Where(x=>x.IsActive == true && x.ActivationDate != null).Count();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return 0;
            }
        }
    }
}

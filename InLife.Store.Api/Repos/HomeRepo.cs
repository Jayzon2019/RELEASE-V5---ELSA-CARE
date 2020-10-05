using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Repos
{
    public class HomeRepo
    {
          InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        LogsRepo lR = new LogsRepo();
        public List<TblHero> GetHeroSliders(ref string log)
        {
            try
            {
                var sliders = db.TblHero.Where(x => x.IsActive == true).OrderBy(x => x.CreatedDate).ToList();
                return sliders;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public List<TblProducts> GetProductsList(ref string log)
        {
            try
            {
                var productsList = db.TblProducts.Where(x => x.IsActive == true).OrderBy(x => x.SortNum).ToList();
                return productsList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public List<TblProductDetails> GetProductsDetailList(ref string log)
        {
            try
            {
                var productsDetailList = db.TblProductDetails.Where(x => x.IsActive == true).ToList();
                return productsDetailList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblKeyMetrics GetPageId_SetPageView(ref string log, TblKeyMetrics key)
        {
            try
            {
                db.TblKeyMetrics.Add(key);
                db.SaveChanges();
                return key;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblKeyMetrics GetPageViewById(ref string log, int id)
        {
            try
            {
                var key = db.TblKeyMetrics.Where(x => x.KeyMetricsId == id).FirstOrDefault();
                return key;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SetUserLeaveTime(ref string log, TblKeyMetrics oldRecord)
        {
            try
            {
                db.TblKeyMetrics.Update(oldRecord);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public List<TblPrimeCare> GetFiles(ref string log)
        {
            try
            {
                return db.TblPrimeCare.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
    }
}

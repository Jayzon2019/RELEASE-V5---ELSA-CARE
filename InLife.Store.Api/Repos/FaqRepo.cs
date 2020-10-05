using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Repos
{
    public class FaqRepo
    {
          InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        LogsRepo lR = new LogsRepo();
        public List<TblFaqCategories> GetFaqCatList(ref string log)
        {
            try
            {
                var faqCat = db.TblFaqCategories.Where(x => x.IsActive == true).OrderByDescending(x=>x.FaqCatId).ToList();
                return faqCat;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public List<TblFaq> GetFaqListByCatId(int id , ref string log)
        {
            try
            {
                var faq = db.TblFaq.Where(x => x.IsActive == true && x.FaqCatId == id).OrderBy(x=>x.SortNum).ToList();
                return faq;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public List<TblFaq> GetFaqList(ref string log)
        {
            try
            {
                var faq = db.TblFaq.Where(x => x.IsActive == true).OrderBy(x=>x.SortNum).ToList();
                return faq;

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

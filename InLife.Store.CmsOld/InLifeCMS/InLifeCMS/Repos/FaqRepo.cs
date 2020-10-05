using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Services;
using InLifeCMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Repos
{
    public class FaqRepo
    {
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();
        public List<FaqViewModel> GetFaqList(ref string log)
        {
            try
            {
                var faqList = (from f in db.TblFaq
                               join fc in db.TblFaqCategories on f.FaqCatId equals fc.FaqCatId 
                               join u in db.TblUsers on f.CreatedBy equals u.UserId
                               where f.IsActive == true
                               orderby f.SortNum ascending
                               select new FaqViewModel
                               {
                                   intFaqCatId = fc.FaqCatId,
                                   strFaqCat = fc.FaqCategory,
                                   intFaqId = f.FaqId,
                                   strFaqQuestion = f.FaqQuestion,
                                   strFaqAnswer = f.FaqAnswer,
                                   intSortNum = f.SortNum,
                                   dteCreatedDate = Comman.getClientTime(fc.CreatedDate.ToString()),
                                   strCreatedByUser = u.FirstName + u.LastName,
                               }).ToList();

                return faqList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblFaq GetFaqById(ref string log, int? id)
        {
            try
            {
                var faq = db.TblFaq.Where(x => x.FaqId == id && x.IsActive == true).FirstOrDefault();
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

        public void SaveFaq(ref string log, TblFaq faq)
        {
            try
            {
                var AddedFaq = db.TblFaq.Add(faq);
                db.SaveChanges();
                if (AddedFaq.Entity.FaqId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Faq Entry ", AddedFaq.Entity.FaqId);
                    LS.SaveActivityLogs(Comman.ActivityActions.Added.ToString(), activityLog);
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void EditFaq(ref string log, TblFaq faq)
        {
            try
            {
                var oldFaq = db.TblFaq.Where(x => x.FaqId == faq.FaqId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldFaq.FaqCatId = faq.FaqCatId;
                oldFaq.FaqId = faq.FaqId;
                oldFaq.FaqAnswer = faq.FaqAnswer;
                oldFaq.FaqQuestion = faq.FaqQuestion;
                oldFaq.SortNum = faq.SortNum;
                oldFaq.UpdatedDate = faq.UpdatedDate;
                oldFaq.UpdatedBy = faq.UpdatedBy;
                db.TblFaq.Update(oldFaq);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Faq Entery", oldFaq.FaqId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteFaq(ref string log, int id, bool delete)
        {
            try
            {
                if (delete)
                {
                    var faq = db.TblFaq.Where(x => x.IsActive == true && x.FaqId == id).FirstOrDefault();
                    if (faq != null)
                    {
                        faq.IsArchived = true;
                        faq.IsActive = false;
                        db.TblFaq.Update(faq);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Faq Entery", faq.FaqId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var faq = db.TblFaq.Where(x => x.IsActive == true && x.FaqId == id).FirstOrDefault();
                    if (faq != null)
                    {
                        faq.IsArchived = false;
                        faq.IsActive = true;
                        db.TblFaq.Update(faq);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Faq Entery", faq.FaqId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deactivated.ToString(), activityLog);
                    }
                }
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public List<TblFaqCategories> GetFaqCategories(ref string log)
        {
            try
            {
                var faqCats = db.TblFaqCategories.Where(x=>x.IsActive == true).ToList();

                return faqCats;
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

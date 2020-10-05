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
    public class FaqCategoriesRepo
    {
      //  dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();
        public List<FaqCategoriesViewModel> GetFCList(ref string log)
        {
            try
            {
                var faqCatList = (from u in db.TblUsers
                                  join fc in db.TblFaqCategories on u.UserId equals fc.CreatedBy
                                  where fc.IsActive == true
                                  orderby fc.CreatedDate descending
                                  select new FaqCategoriesViewModel
                                  {
                                      intFaqCatId = fc.FaqCatId,
                                       strFaqCatDescription = fc.FaqCatDescription,
                                       strFaqCategory = fc.FaqCategory,
                                      dteCreatedDate = Comman.getClientTime(fc.CreatedDate.ToString()),
                                      strCreatedByUser = u.FirstName + " " +u.LastName,
                                  }).ToList();
                return faqCatList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblFaqCategories GetFaqCatById(ref string log, int? id)
        {
            try
            {
                var faqCat = db.TblFaqCategories.Where(x => x.FaqCatId == id && x.IsActive == true).FirstOrDefault();
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

        public void SaveFaqCat(ref string log, TblFaqCategories faqCat)
        {
            try
            {
                var AddedFaqCat = db.TblFaqCategories.Add(faqCat);
                db.SaveChanges();
                if (AddedFaqCat.Entity.FaqCatId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Faq Category", AddedFaqCat.Entity.FaqCatId);
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


        public void EditFaqCat(ref string log, TblFaqCategories faqCat)
        {
            try
            {
                var oldFaqCat = db.TblFaqCategories.Where(x => x.FaqCatId == faqCat.FaqCatId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldFaqCat.FaqCatId = faqCat.FaqCatId;
                oldFaqCat.FaqCategory = faqCat.FaqCategory;
                oldFaqCat.FaqCatDescription = faqCat.FaqCatDescription;
                oldFaqCat.UpdatedDate = faqCat.UpdatedDate;
                oldFaqCat.UpdatedBy = faqCat.UpdatedBy;
                db.TblFaqCategories.Update(oldFaqCat);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Faq Category", oldFaqCat.FaqCatId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteFaqCategory(ref string log, int id, bool delete)
        {
            try
            {
                if (delete)
                {
                    var faqCat = db.TblFaqCategories.Where(x => x.IsActive == true && x.FaqCatId == id).FirstOrDefault();
                    if (faqCat != null)
                    {
                        faqCat.IsArchived = true;
                        faqCat.IsActive = false;
                        db.TblFaqCategories.Update(faqCat);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Faq Category", faqCat.FaqCatId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var faqCat = db.TblFaqCategories.Where(x => x.IsActive == true && x.FaqCatId == id).FirstOrDefault();
                    if (faqCat != null)
                    {
                        faqCat.IsArchived = false;
                        faqCat.IsActive = true;
                        db.TblFaqCategories.Update(faqCat);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Faq Category", faqCat.FaqCatId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Added.ToString(), activityLog);
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


    }
}

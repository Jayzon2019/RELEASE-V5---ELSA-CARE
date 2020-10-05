using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Repos;
using InLifeCMS.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Services
{
    public class FaqCategoriesService
    {
        FaqCategoriesRepo FCR = new FaqCategoriesRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<FaqCategoriesViewModel> GetFaqCatList(ref string log)
        {
            try
            {
                var FaqCatList = FCR.GetFCList(ref log);
                return FaqCatList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public FaqCategoriesViewModel GetFaqCatById(ref string log, int? id)
        {
            try
            {
                var faq = FCR.GetFaqCatById(ref log, id);

                FaqCategoriesViewModel FCVM = new FaqCategoriesViewModel
                {
                    blnIsActive = faq.IsActive,
                    intCreatedBy = faq.CreatedBy,
                    blnIsArchived = faq.IsArchived,
                    intUpdatedBy = faq.UpdatedBy,
                    intFaqCatId = faq.FaqCatId,
                    strFaqCategory = faq.FaqCategory,
                    strFaqCatDescription = faq.FaqCatDescription,
                };
                if (faq.CreatedDate != null)
                {
                    FCVM.dteCreatedDate = Comman.getClientTime(faq.CreatedDate.ToString());
                }
                if (faq.UpdatedDate != null)
                {
                    FCVM.dteUpdatedDate = Comman.getClientTime(faq.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(FCVM.intCreatedBy));
                FCVM.strCreatedByUser = createdBy;
                if (FCVM.intUpdatedBy > 0)
                {
                    if (FCVM.intCreatedBy != FCVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(FCVM.intUpdatedBy);
                        FCVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        FCVM.strUpdatedByUser = createdBy;
                    }
                }

                return FCVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public string SaveFaqCat(ref string log, FaqCategoriesViewModel faqCatVM)
        {
            try
            {
                TblFaqCategories faqCat = new TblFaqCategories
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    FaqCatId = faqCatVM.intFaqCatId,
                    FaqCatDescription = faqCatVM.strFaqCatDescription,
                    FaqCategory = faqCatVM.strFaqCategory,
                };
                FCR.SaveFaqCat(ref log, faqCat);
                return "Saved";
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return Comman.SomethingWntWrong;
            }
        }



        public void EditFaqCat(ref string log, FaqCategoriesViewModel faqCat)
        {
            try
            {
                TblFaqCategories fc = new TblFaqCategories
                {
                     FaqCatId = faqCat.intFaqCatId,
                     FaqCategory  = faqCat.strFaqCategory,
                     FaqCatDescription = faqCat.strFaqCatDescription,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
               
                FCR.EditFaqCat(ref log, fc);

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
                FCR.Deactivate_DeleteFaqCategory(ref log, id, delete);
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

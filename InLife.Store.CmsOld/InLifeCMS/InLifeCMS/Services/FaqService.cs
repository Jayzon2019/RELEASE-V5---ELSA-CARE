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
    public class FaqService
    {
        FaqRepo FR = new FaqRepo();
        FaqCategoriesRepo FCR = new FaqCategoriesRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<FaqViewModel> GetFaqList(ref string log)
        {
            try
            {
                var FList = FR.GetFaqList(ref log);
                return FList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public string SaveFaq(ref string log, FaqViewModel faqVM)
        {
            try
            {
                TblFaq faq = new TblFaq
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    FaqCatId = faqVM.intFaqCatId,
                     FaqQuestion = faqVM.strFaqQuestion,
                    FaqAnswer = faqVM.strFaqAnswer,
                    SortNum = faqVM.intSortNum,
                };
                    FR.SaveFaq(ref log, faq);
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

        public FaqViewModel GetFaqById(ref string log, int? id)
        {
            try
            {
                var faq = FR.GetFaqById(ref log, id);

                FaqViewModel FVM = new FaqViewModel
                {
                    blnIsActive = faq.IsActive,
                    intCreatedBy = faq.CreatedBy,
                    blnIsArchived = faq.IsArchived,
                    intUpdatedBy = faq.UpdatedBy,
                    intFaqCatId = faq.FaqCatId,
                     intFaqId = faq.FaqId,
                     strFaqQuestion = faq.FaqQuestion,
                     strFaqAnswer = faq.FaqAnswer,
                     intSortNum = faq.SortNum,
                };
                if (faq.CreatedDate != null)
                {
                    FVM.dteCreatedDate = Comman.getClientTime(faq.CreatedDate.ToString());
                }
                if (faq.UpdatedDate != null)
                {
                    FVM.dteUpdatedDate = Comman.getClientTime(faq.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(FVM.intCreatedBy));
                FVM.strCreatedByUser = createdBy;
                if (FVM.intUpdatedBy > 0)
                {
                    if (FVM.intCreatedBy != FVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(FVM.intUpdatedBy);
                        FVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        FVM.strUpdatedByUser = createdBy;
                    }
                }
                var faqCat = FCR.GetFaqCatById(ref log, FVM.intFaqCatId);
                FVM.strFaqCat = faqCat.FaqCategory;
                return FVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public string EditFaq(ref string log, FaqViewModel faq)
        {
            try
            {
                TblFaq f = new TblFaq
                {
                    FaqCatId = faq.intFaqCatId,
                    FaqId = faq.intFaqId,
                    FaqAnswer = faq.strFaqAnswer,
                     FaqQuestion = faq.strFaqQuestion,
                     SortNum = faq.intSortNum,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
                    FR.EditFaq(ref log, f);
                    return "Updated";
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return Comman.SomethingWntWrong;
            }
        }

        public void Deactivate_DeleteFaq(ref string log, int id, bool delete)
        {
            try
            {
              FR.Deactivate_DeleteFaq(ref log, id, delete);
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

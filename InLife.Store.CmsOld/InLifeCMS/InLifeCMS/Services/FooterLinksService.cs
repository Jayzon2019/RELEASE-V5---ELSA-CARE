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
    public class FooterLinksService
    {
        FooterLinksRepo FLR = new FooterLinksRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<FooterLinksViewModel> GetFooterLinksList(ref string log)
        {
            try
            {

                List<FooterLinksViewModel> FLList = new List<FooterLinksViewModel>();
                var FList = FLR.GetFooterLinksList(ref log);
                foreach (var links in FList)
                {
                    FooterLinksViewModel FL = new FooterLinksViewModel
                    {
                        intFooterLinkId = links.FooterLinkId,
                         strContactUsUrl = links.ContactUsUrl,
                         strCusCharterUrl = links.CusCharterUrl,
                         strInsCommissionUrl = links.InsCommissionUrl,
                         strFbUrl = links.FbUrl,
                         strInstaUrl = links.InstaUrl,
                         strLogoUrl = links.LogoUrl,
                         strMainSiteUrl = links.MainSiteUrl,
                         strPrivacyPolicyUrl = links.PrivacyPolicyUrl,
                          strTermsConditionUrl = links.TermsConditionUrl,
                          strTweeterUrl = links.TweeterUrl,
                          strYouTubeUrl = links.YouTubeUrl,
                        strMainSiteTxt = links.MainSiteTxt,
                        strInsCommissionTxt = links.InsCommissionTxt,
                        strCusCharterTxt = links.CusCharterTxt,
                        strTermsConditionTxt = links.TermsConditionTxt,
                        strPrivacyPolicyTxt = links.PrivacyPolicyTxt,
                        strContactUsTxt = links.ContactUsTxt
                    };
                    FLList.Add(FL);
                }
                return FLList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public FooterLinksViewModel GetFooterLinkById(ref string log, int? id)
        {
            try
            {
                var footerLink = FLR.GetFooterLinkById(ref log, id);

                FooterLinksViewModel FLVM = new FooterLinksViewModel
                {
                    blnIsActive = footerLink.IsActive,
                    intCreatedBy = footerLink.CreatedBy,
                    intUpdatedBy = footerLink.UpdatedBy,
                    intFooterLinkId = footerLink.FooterLinkId,
                    strContactUsUrl = footerLink.ContactUsUrl,
                    strCusCharterUrl = footerLink.CusCharterUrl,
                    strInsCommissionUrl = footerLink.InsCommissionUrl,
                    strFbUrl = footerLink.FbUrl,
                    strInstaUrl = footerLink.InstaUrl,
                    strLogoUrl = footerLink.LogoUrl,
                    strMainSiteUrl = footerLink.MainSiteUrl,
                    strPrivacyPolicyUrl = footerLink.PrivacyPolicyUrl,
                    strTermsConditionUrl = footerLink.TermsConditionUrl,
                    strTweeterUrl = footerLink.TweeterUrl,
                    strYouTubeUrl = footerLink.YouTubeUrl,
                    strMainSiteTxt = footerLink.MainSiteTxt,
                    strInsCommissionTxt = footerLink.InsCommissionTxt,
                    strCusCharterTxt = footerLink.CusCharterTxt,
                    strTermsConditionTxt = footerLink.TermsConditionTxt,
                    strPrivacyPolicyTxt = footerLink.PrivacyPolicyTxt,
                    strContactUsTxt = footerLink.ContactUsTxt
                };
                if (footerLink.CreatedDate != null)
                {
                    FLVM.dteCreatedDate = Comman.getClientTime(footerLink.CreatedDate.ToString());
                }
                if (footerLink.UpdatedDate != null)
                {
                    FLVM.dteUpdatedDate = Comman.getClientTime(footerLink.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(FLVM.intCreatedBy));
                FLVM.strCreatedByUser = createdBy;
                if (FLVM.intUpdatedBy > 0)
                {
                    if (FLVM.intCreatedBy != FLVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(FLVM.intUpdatedBy);
                        FLVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        FLVM.strUpdatedByUser = createdBy;
                    }
                }

                return FLVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
          public FooterLinksViewModel GetFooterLink(ref string log)
        {
            try
            {
                var footerLink = FLR.GetFooterLink(ref log);

                FooterLinksViewModel FLVM = new FooterLinksViewModel
                {
                    blnIsActive = footerLink.IsActive,
                    intCreatedBy = footerLink.CreatedBy,
                    intUpdatedBy = footerLink.UpdatedBy,
                    intFooterLinkId = footerLink.FooterLinkId,
                    strContactUsUrl = footerLink.ContactUsUrl,
                    strCusCharterUrl = footerLink.CusCharterUrl,
                    strInsCommissionUrl = footerLink.InsCommissionUrl,
                    strFbUrl = footerLink.FbUrl,
                    strInstaUrl = footerLink.InstaUrl,
                    strLogoUrl = footerLink.LogoUrl,
                    strMainSiteUrl = footerLink.MainSiteUrl,
                    strPrivacyPolicyUrl = footerLink.PrivacyPolicyUrl,
                    strTermsConditionUrl = footerLink.TermsConditionUrl,
                    strTweeterUrl = footerLink.TweeterUrl,
                    strYouTubeUrl = footerLink.YouTubeUrl,
                    strMainSiteTxt = footerLink.MainSiteTxt,
                    strInsCommissionTxt = footerLink.InsCommissionTxt,
                    strCusCharterTxt = footerLink.CusCharterTxt,
                    strTermsConditionTxt = footerLink.TermsConditionTxt,
                    strPrivacyPolicyTxt = footerLink.PrivacyPolicyTxt,
                    strContactUsTxt = footerLink.ContactUsTxt

                };
                if (footerLink.CreatedDate != null)
                {
                    FLVM.dteCreatedDate = Comman.getClientTime(footerLink.CreatedDate.ToString());
                }
                if (footerLink.UpdatedDate != null)
                {
                    FLVM.dteUpdatedDate = Comman.getClientTime(footerLink.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(FLVM.intCreatedBy));
                FLVM.strCreatedByUser = createdBy;
                if (FLVM.intUpdatedBy > 0)
                {
                    if (FLVM.intCreatedBy != FLVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(FLVM.intUpdatedBy);
                        FLVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        FLVM.strUpdatedByUser = createdBy;
                    }
                }

                return FLVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public string SaveFooterLink(ref string log, FooterLinksViewModel footerVM)
        {
            try
            {
                TblFooterLinks footerLink = new TblFooterLinks
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    FooterLinkId = footerVM.intFooterLinkId,
                    ContactUsUrl = footerVM.strContactUsUrl,
                    CusCharterUrl = footerVM.strCusCharterUrl,
                    InsCommissionUrl = footerVM.strInsCommissionUrl,
                    FbUrl = footerVM.strFbUrl,
                    InstaUrl = footerVM.strInstaUrl,
                    LogoUrl = footerVM.strLogoUrl,
                    MainSiteUrl = footerVM.strMainSiteUrl,
                    PrivacyPolicyUrl = footerVM.strPrivacyPolicyUrl,
                    TermsConditionUrl = footerVM.strTermsConditionUrl,
                    TweeterUrl = footerVM.strTweeterUrl,
                    YouTubeUrl = footerVM.strYouTubeUrl,
                    MainSiteTxt = footerVM.strMainSiteTxt,
                    InsCommissionTxt = footerVM.strInsCommissionTxt,
                    CusCharterTxt = footerVM.strCusCharterTxt,
                    TermsConditionTxt = footerVM.strTermsConditionTxt,
                    PrivacyPolicyTxt = footerVM.strPrivacyPolicyTxt,
                    ContactUsTxt = footerVM.strContactUsTxt

                };
                FLR.SaveFooterLink(ref log, footerLink);
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


        public string EditFooterLink(ref string log, FooterLinksViewModel footer)
        {
            try
            {
                TblFooterLinks f = new TblFooterLinks
                {
                    FooterLinkId = footer.intFooterLinkId,
                    ContactUsUrl = footer.strContactUsUrl,
                    CusCharterUrl = footer.strCusCharterUrl,
                    InsCommissionUrl = footer.strInsCommissionUrl,
                    FbUrl = footer.strFbUrl,
                    InstaUrl = footer.strInstaUrl,
                    LogoUrl = footer.strLogoUrl,
                    MainSiteUrl = footer.strMainSiteUrl,
                    PrivacyPolicyUrl = footer.strPrivacyPolicyUrl,
                    TermsConditionUrl = footer.strTermsConditionUrl,
                    TweeterUrl = footer.strTweeterUrl,
                    YouTubeUrl = footer.strYouTubeUrl,
                    MainSiteTxt = footer.strMainSiteTxt,
                    InsCommissionTxt = footer.strInsCommissionTxt,
                    CusCharterTxt = footer.strCusCharterTxt,
                    TermsConditionTxt = footer.strTermsConditionTxt,
                    PrivacyPolicyTxt = footer.strPrivacyPolicyTxt,
                    ContactUsTxt = footer.strContactUsTxt,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
               FLR.EditFooterLink(ref log, f);
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

        public void Deactivate_DeleteFooterLink(ref string log, int id)
        {
            try
            {
                FLR.Deactivate_DeleteFooterLink(ref log, id);
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

using InLifeCMS.Helpers;
using InLifeCMS.Services;
using InLifeCMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Repos
{
    public class FooterLinksRepo
    {
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
          InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();
        public List<TblFooterLinks> GetFooterLinksList(ref string log)
        {
            try
            {
                var footerLinksList = db.TblFooterLinks.Where(x => x.IsActive == true).ToList();
                return footerLinksList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblFooterLinks GetFooterLinkById(ref string log, int? id)
        {
            try
            {
                var footerLink = db.TblFooterLinks.Where(x => x.FooterLinkId == id && x.IsActive == true).FirstOrDefault();
                return footerLink;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public TblFooterLinks GetFooterLink(ref string log)
        {
            try
            {
                var footerLink = db.TblFooterLinks.Where(x => x.IsActive == true).FirstOrDefault();
                return footerLink;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SaveFooterLink(ref string log, TblFooterLinks footerLink)
        {
            try
            {
                var AddedFooterLinks = db.TblFooterLinks.Add(footerLink);
                db.SaveChanges();
                if (AddedFooterLinks.Entity.FooterLinkId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Footer link ", AddedFooterLinks.Entity.FooterLinkId);
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

        public void EditFooterLink(ref string log, TblFooterLinks footerLink)
        {
            try
            {
                var oldFooterLink = db.TblFooterLinks.Where(x => x.FooterLinkId == footerLink.FooterLinkId && x.IsActive == true).FirstOrDefault();
                oldFooterLink.FooterLinkId = footerLink.FooterLinkId;
                oldFooterLink.ContactUsUrl = footerLink.ContactUsUrl;
                oldFooterLink.CusCharterUrl = footerLink.CusCharterUrl;
                oldFooterLink.FbUrl = footerLink.FbUrl;
                oldFooterLink.InsCommissionUrl = footerLink.InsCommissionUrl;
                oldFooterLink.InstaUrl = footerLink.InstaUrl;
                oldFooterLink.LogoUrl = footerLink.LogoUrl;
                oldFooterLink.MainSiteUrl = footerLink.MainSiteUrl;
                oldFooterLink.PrivacyPolicyUrl = footerLink.PrivacyPolicyUrl;
                oldFooterLink.TermsConditionUrl = footerLink.TermsConditionUrl;
                oldFooterLink.TweeterUrl = footerLink.TweeterUrl;
                oldFooterLink.YouTubeUrl = footerLink.YouTubeUrl;
                oldFooterLink.MainSiteTxt = footerLink.MainSiteTxt;
                oldFooterLink.InsCommissionTxt = footerLink.InsCommissionTxt;
                oldFooterLink.CusCharterTxt = footerLink.CusCharterTxt;
                oldFooterLink.TermsConditionTxt = footerLink.TermsConditionTxt;
                oldFooterLink.PrivacyPolicyTxt = footerLink.PrivacyPolicyTxt;
                oldFooterLink.ContactUsTxt = footerLink.ContactUsTxt;
                oldFooterLink.UpdatedDate = footerLink.UpdatedDate;
                oldFooterLink.UpdatedBy = footerLink.UpdatedBy;
                db.TblFooterLinks.Update(oldFooterLink);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), " Footer Link Entery", oldFooterLink.FooterLinkId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteFooterLink(ref string log, int id)
        {
            try
            {
                var footerLink = db.TblFooterLinks.Where(x => x.IsActive == true && x.FooterLinkId == id).FirstOrDefault();
                if (footerLink != null)
                {
                    footerLink.IsActive = false;
                    db.TblFooterLinks.Update(footerLink);
                    db.SaveChanges();
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), " Footer Link Entery", footerLink.FooterLinkId);
                    LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
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

using InLifeCMS.Helpers;
using InLifeCMS.Repos;
using InLifeCMS.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Services
{
    public class PrimeHeroService
    {
        PrimeHeroRepo PHR = new PrimeHeroRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<PrimeHeroViewModel> GetPrimeHeroSliders(ref string log)
        {
            try
            {
                List<PrimeHeroViewModel> PHVMList = new List<PrimeHeroViewModel>();
                var primeHeroSliders = PHR.GetPrimeHeroSliders(ref log);
                foreach (var hero in primeHeroSliders)
                {
                    PrimeHeroViewModel PHVM = new PrimeHeroViewModel
                    {
                        intPrimeHeroId = hero.PrimeHeroId,
                        strBtnTxtLink = hero.BtnTxtLink,
                        strPrimeHeroBg = hero.PrimeHeroBg,
                        strPrimeHeroBtnTxt = hero.PrimeHeroBtnTxt,
                        strPrimeHeroTitle = hero.PrimeHeroTitle,
                        strHeading = hero.Heading,
                        strSubHeading = hero.SubHeading,
                        strContentPostion = hero.ContentPostion,
                        strHeadingColor = hero.HeadingColor,
                        strSubHeadingColor = hero.SubHeadingColor,
                    };
                    PHVMList.Add(PHVM);
                }
                return PHVMList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public string SavePrimeHeroSlider(ref string log, PrimeHeroViewModel primeHeroVM)
        {
            try
            {
                var uploadPathWithfileName = "";
                var uploadPathWithfileName2 = "";
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                var file = files[0];
                if (file != null && file.Length > 0)
                {
                    uploadPathWithfileName = Comman.ConvertImageToBase64String(file);
                }
                var SndFile = files[1];
                if (SndFile != null && SndFile.Length > 0)
                {
                    uploadPathWithfileName2 = Comman.ConvertImageToBase64String(SndFile);
                }
                TblPrimeHero PrimeHero = new TblPrimeHero
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    BtnTxtLink = primeHeroVM.strBtnTxtLink,
                    PrimeHeroBg = primeHeroVM.strPrimeHeroBg,
                    PrimeHeroBtnTxt = primeHeroVM.strPrimeHeroBtnTxt,
                    PrimeHeroId = primeHeroVM.intPrimeHeroId,
                    PrimeHeroTitle = primeHeroVM.strPrimeHeroTitle,
                     Heading = primeHeroVM.strHeading,
                      SubHeading = primeHeroVM.strSubHeading,
                      ContentPostion = primeHeroVM.strContentPostion,
                      SubHeadingColor = primeHeroVM.strSubHeadingColor,
                      HeadingColor = primeHeroVM.strHeadingColor,
                };
                PrimeHero.PrimeHeroBg = uploadPathWithfileName;
                PrimeHero.PrimeHeroMobBg = uploadPathWithfileName2;
                PHR.SavePrimeHeroSlider(ref log, PrimeHero);
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

        public PrimeHeroViewModel GetPrimeHeroSliderById(ref string log, int? id)
        {
            try
            {
                var primeHero = PHR.GetPrimeHeroSliderById(ref log, id);

                PrimeHeroViewModel PHVM = new PrimeHeroViewModel
                {
                    blnIsActive = primeHero.IsActive,
                    intCreatedBy = primeHero.CreatedBy,
                    intPrimeHeroId = primeHero.PrimeHeroId,
                    intUpdatedBy = primeHero.UpdatedBy,
                    strBtnTxtLink = primeHero.BtnTxtLink,
                    strPrimeHeroBg = primeHero.PrimeHeroBg,
                    strPrimeHeroMobBg = primeHero.PrimeHeroMobBg,
                    strPrimeHeroBtnTxt = primeHero.PrimeHeroBtnTxt,
                    strPrimeHeroTitle = primeHero.PrimeHeroTitle,
                    strHeading = primeHero.Heading,
                    strSubHeading = primeHero.SubHeading,
                      strContentPostion = primeHero.ContentPostion,
                    strHeadingColor = primeHero.HeadingColor,
                    strSubHeadingColor = primeHero.SubHeadingColor,
                };
                if (primeHero.CreatedDate != null)
                {
                    PHVM.dteCreatedDate = Comman.getClientTime(primeHero.CreatedDate.ToString());
                }
                if (primeHero.UpdatedDate != null)
                {
                    PHVM.dteUpdatedDate = Comman.getClientTime(primeHero.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(PHVM.intCreatedBy));
                PHVM.strCreatedByUser = createdBy;
                if (PHVM.intUpdatedBy > 0)
                {
                    if (PHVM.intCreatedBy != PHVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(PHVM.intUpdatedBy);
                        PHVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        PHVM.strUpdatedByUser = createdBy;
                    }
                }

                return PHVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditPrimeHeroSlider(ref string log, PrimeHeroViewModel primeHero)
        {
            try
            {
                TblPrimeHero ph = new TblPrimeHero
                {
                    BtnTxtLink = primeHero.strBtnTxtLink,
                    PrimeHeroBg = primeHero.strPrimeHeroBg,
                    PrimeHeroBtnTxt = primeHero.strPrimeHeroBtnTxt,
                    PrimeHeroId = primeHero.intPrimeHeroId,
                    PrimeHeroTitle = primeHero.strPrimeHeroTitle,
                    Heading = primeHero.strHeading,
                    SubHeading = primeHero.strSubHeading,
                    ContentPostion = primeHero.strContentPostion,
                    SubHeadingColor = primeHero.strSubHeadingColor,
                    HeadingColor = primeHero.strHeadingColor,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var file = files[0];
                    if (file != null && file.Length > 0)
                    {
                        var uploadPathWithfileName = Comman.ConvertImageToBase64String(file);
                        if (file.Name == "fileMob")
                        {
                            ph.PrimeHeroMobBg = uploadPathWithfileName;
                        }
                        else
                        {
                            ph.PrimeHeroBg = uploadPathWithfileName;
                        }
                    }
                    if (files.Count > 1)
                    {
                        var SndFile = files[1];
                        if (SndFile != null && SndFile.Length > 0)
                        {
                            var uploadPathWithfileName2 = Comman.ConvertImageToBase64String(SndFile);
                            ph.PrimeHeroMobBg = uploadPathWithfileName2;
                        }
                    }
                }
                PHR.EditPrimeHeroSlider(ref log, ph);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeletePrimeHeroSlider(ref string log, int id, bool delete)
        {
            try
            {
                PHR.Deactivate_DeletePrimeHeroSlider(ref log, id, delete);
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

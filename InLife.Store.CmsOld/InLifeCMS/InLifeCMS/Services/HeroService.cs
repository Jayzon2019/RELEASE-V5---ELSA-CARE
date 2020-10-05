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
    public class HeroService
    {
        HeroRepo HR = new HeroRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<HeroViewModel> GetHeroSliders(ref string log)
        {
            try
            {
                List<HeroViewModel> HVMList = new List<HeroViewModel>();
                var heroSliders = HR.GetHeroSliders(ref log);
                foreach (var hero in heroSliders)
                {
                    HeroViewModel HVM = new HeroViewModel
                    {
                        intHeroId = hero.HeroId,
                        strBtnTxtLink = hero.BtnTxtLink,
                        strHeroBg = hero.HeroBg,
                        strHeroBtnTxt = hero.HeroBtnTxt,
                        strHeroTitle = hero.HeroTitle,
                         strHeading = hero.Heading,
                        strSubHeading = hero.SubHeading,
                        strHeadingColor = hero.HeadingColor,
                        strSubHeadingColor = hero.SubHeadingColor,
                        strContentPostion = hero.ContentPostion,
                    };
                    HVMList.Add(HVM);
                }
                return HVMList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public string SaveHeroSlider(ref string log, HeroViewModel heroVM)
        {
            try
            {
                var uploadPathWithfileName = "";
                var uploadPathWithfileName2 = "";
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
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
                }
                TblHero hero = new TblHero
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    BtnTxtLink = heroVM.strBtnTxtLink,
                    HeroBg = heroVM.strHeroBg,
                    HeroBtnTxt = heroVM.strHeroBtnTxt,
                    HeroId = heroVM.intHeroId,
                    HeroTitle = heroVM.strHeroTitle,
                    Heading = heroVM.strHeading,
                    SubHeading = heroVM.strSubHeading,
                    HeadingColor = heroVM.strHeadingColor,
                    SubHeadingColor = heroVM.strSubHeadingColor,
                    ContentPostion = heroVM.strContentPostion,
                    
                };
                hero.HeroBg = uploadPathWithfileName;
                hero.HeroMobBg = uploadPathWithfileName2;
                HR.SaveHeroSlider(ref log, hero);
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

        public HeroViewModel GetHeroSliderById(ref string log, int? id)
        {
            try
            {
                var hero = HR.GetHeroSliderById(ref log, id);

                HeroViewModel HVM = new HeroViewModel
                {
                    blnIsActive = hero.IsActive,
                    intCreatedBy = hero.CreatedBy,
                    intHeroId = hero.HeroId,
                    intUpdatedBy = hero.UpdatedBy,
                    strBtnTxtLink = hero.BtnTxtLink,
                    strHeroBg = hero.HeroBg,
                    strHeroMobBg = hero.HeroMobBg,
                    strHeroBtnTxt = hero.HeroBtnTxt,
                    strHeroTitle = hero.HeroTitle,
                    strHeading = hero.Heading,
                    strSubHeading = hero.SubHeading,
                    strHeadingColor = hero.HeadingColor,
                    strSubHeadingColor = hero.SubHeadingColor,
                    strContentPostion = hero.ContentPostion,
                };
                if (hero.CreatedDate != null)
                {
                    HVM.dteCreatedDate = Comman.getClientTime(hero.CreatedDate.ToString());
                }
                if (hero.UpdatedDate != null)
                {
                    HVM.dteUpdatedDate = Comman.getClientTime(hero.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(HVM.intCreatedBy));
                HVM.strCreatedByUser = createdBy;
                if (HVM.intUpdatedBy > 0)
                {
                    if (HVM.intCreatedBy != HVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(HVM.intUpdatedBy);
                        HVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        HVM.strUpdatedByUser = createdBy;
                    }
                }

                return HVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditHeroSlider(ref string log, HeroViewModel hero)
        {
            try
            {
                TblHero h = new TblHero
                {
                    BtnTxtLink = hero.strBtnTxtLink,
                    HeroBg = hero.strHeroBg,
                    HeroBtnTxt = hero.strHeroBtnTxt,
                    HeroId = hero.intHeroId,
                    HeroTitle = hero.strHeroTitle,
                    Heading = hero.strHeading,
                    SubHeading = hero.strSubHeading,
                    HeadingColor = hero.strHeadingColor,
                    SubHeadingColor = hero.strSubHeadingColor,
                    ContentPostion = hero.strContentPostion,
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
                            h.HeroMobBg = uploadPathWithfileName;
                        }
                        else
                        {
                            h.HeroBg = uploadPathWithfileName;
                        }
                    }
                    if (files.Count > 1)
                    {
                        var SndFile = files[1];
                        if (SndFile != null && SndFile.Length > 0)
                        {
                            var uploadPathWithfileName2 = Comman.ConvertImageToBase64String(SndFile);
                            h.HeroMobBg = uploadPathWithfileName2;
                        }
                    }
                }
                HR.EditHeroSlider(ref log, h);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteHeroSlider(ref string log, int id, bool delete)
        {
            try
            {
                HR.Deactivate_DeleteHeroSlider(ref log, id, delete);
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

using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Repos
{
    public class HeroRepo
    {
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();

        public List<TblHero> GetHeroSliders(ref string log)
        {
            try
            {
                var heroSliders = db.TblHero.Where(x => x.IsActive == true).OrderByDescending(x=>x.CreatedDate).ToList();

                return heroSliders;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SaveHeroSlider(ref string log, TblHero hero)
        {
            try
            {
                var AddedHero = db.TblHero.Add(hero);
                db.SaveChanges();
                if (AddedHero.Entity.HeroId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Hero Slider", AddedHero.Entity.HeroId);
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

        public TblHero GetHeroSliderById(ref string log, int? id)
        {
            try
            {
                var hero = db.TblHero.Where(x => x.HeroId == id && x.IsActive == true).FirstOrDefault();
                return hero;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditHeroSlider(ref string log, TblHero hero)
        {
            try
            {
                var oldhero = db.TblHero.Where(x => x.HeroId == hero.HeroId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldhero.BtnTxtLink = hero.BtnTxtLink;
                if (hero.HeroBg != null && hero.HeroBg != "")
                {
                    oldhero.HeroBg = hero.HeroBg;
                }
                if (hero.HeroMobBg != null && hero.HeroMobBg != "")
                {
                    oldhero.HeroMobBg = hero.HeroMobBg;
                }
                oldhero.HeroBtnTxt = hero.HeroBtnTxt;
                oldhero.HeroId = hero.HeroId;
                oldhero.HeroTitle = hero.HeroTitle;
                oldhero.Heading = hero.Heading;
                oldhero.SubHeading = hero.SubHeading;
                oldhero.HeadingColor = hero.HeadingColor;
                oldhero.SubHeadingColor = hero.SubHeadingColor;
                oldhero.ContentPostion = hero.ContentPostion;
                oldhero.UpdatedDate = hero.UpdatedDate;
                oldhero.UpdatedBy = hero.UpdatedBy;
                db.TblHero.Update(oldhero);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Hero Slider", oldhero.HeroId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
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
                if (delete)
                {
                    var hero = db.TblHero.Where(x => x.IsActive == true && x.HeroId == id).FirstOrDefault();
                    if (hero != null)
                    {
                        hero.IsArchived = true;
                        hero.IsActive = false;
                        db.TblHero.Update(hero);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Hero Slider", hero.HeroId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var hero = db.TblHero.Where(x => x.IsActive == true && x.HeroId == id).FirstOrDefault();
                    if (hero != null)
                    {
                        hero.IsArchived = false;
                        hero.IsActive = true;
                        db.TblHero.Update(hero);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Hero Slider", hero.HeroId);
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

    }
}

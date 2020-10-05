using InLifeCMS.Helpers;
using InLifeCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Repos
{
    public class PrimeHeroRepo
    {
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();

        public List<TblPrimeHero> GetPrimeHeroSliders(ref string log)
        {
            try
            {
                var primeHeroSliders = db.TblPrimeHero.Where(x => x.IsActive == true).OrderByDescending(x=>x.CreatedDate).ToList();

                return primeHeroSliders;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SavePrimeHeroSlider(ref string log, TblPrimeHero primeHero)
        {
            try
            {
                var AddedPrimeHero = db.TblPrimeHero.Add(primeHero);
                db.SaveChanges();
                if (AddedPrimeHero.Entity.PrimeHeroId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Prime Hero Slider", AddedPrimeHero.Entity.PrimeHeroId);
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

        public TblPrimeHero GetPrimeHeroSliderById(ref string log, int? id)
        {
            try
            {
                var primeHero = db.TblPrimeHero.Where(x => x.PrimeHeroId == id && x.IsActive == true).FirstOrDefault();
                return primeHero;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditPrimeHeroSlider(ref string log, TblPrimeHero primeHero)
        {
            try
            {
                var oldhero = db.TblPrimeHero.Where(x => x.PrimeHeroId == primeHero.PrimeHeroId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldhero.BtnTxtLink = primeHero.BtnTxtLink;
                if (primeHero.PrimeHeroBg != null && primeHero.PrimeHeroBg != "")
                {
                oldhero.PrimeHeroBg = primeHero.PrimeHeroBg;
                }
                if (primeHero.PrimeHeroMobBg != null && primeHero.PrimeHeroMobBg != "")
                {
                oldhero.PrimeHeroMobBg = primeHero.PrimeHeroMobBg;
                }
                oldhero.PrimeHeroBtnTxt = primeHero.PrimeHeroBtnTxt;
                oldhero.PrimeHeroId = primeHero.PrimeHeroId;
                oldhero.PrimeHeroTitle = primeHero.PrimeHeroTitle;
                oldhero.Heading = primeHero.Heading;
                oldhero.SubHeading = primeHero.SubHeading;
                oldhero.ContentPostion = primeHero.ContentPostion;
                oldhero.HeadingColor = primeHero.HeadingColor;
                oldhero.SubHeadingColor = primeHero.SubHeadingColor;
                oldhero.UpdatedDate = primeHero.UpdatedDate;
                oldhero.UpdatedBy = primeHero.UpdatedBy;
                db.TblPrimeHero.Update(oldhero);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Prime Hero Slider", oldhero.PrimeHeroId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
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
                if (delete)
                {
                    var hero = db.TblPrimeHero.Where(x => x.IsActive == true && x.PrimeHeroId == id).FirstOrDefault();
                    if (hero != null)
                    {
                        hero.IsArchived = true;
                        hero.IsActive = false;
                        db.TblPrimeHero.Update(hero);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Prime Hero Slider", hero.PrimeHeroId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var hero = db.TblPrimeHero.Where(x => x.IsActive == true && x.PrimeHeroId == id).FirstOrDefault();
                    if (hero != null)
                    {
                        hero.IsArchived = false;
                        hero.IsActive = true;
                        db.TblPrimeHero.Update(hero);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Prime Hero Slider", hero.PrimeHeroId);
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

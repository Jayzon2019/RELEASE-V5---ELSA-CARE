using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Repos
{
    public class PrimeHomeRepo
    {
          InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        LogsRepo lR = new LogsRepo();
        public List<TblPrimeHero> GetPrimeHeroSliders(ref string log)
        {
            try
            {
                var sliders = db.TblPrimeHero.Where(x => x.IsActive == true).OrderBy(x => x.CreatedDate).ToList();
                return sliders;

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

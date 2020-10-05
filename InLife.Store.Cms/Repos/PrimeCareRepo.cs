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
    public class PrimeCareRepo
    {
       // dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();

        public List<PrimeCareViewModel> GetPrimeCareFiles(ref string log)
        {
            try
            {
                var primeCareFiles = (from u in db.TblUsers
                                      join p in db.TblPrimeCare on u.UserId equals p.CreatedBy
                                      where p.IsActive == true
                                      orderby p.CreatedDate descending
                                      select new PrimeCareViewModel
                                      {
                                          intPrimeCareId = p.PrimeCareId,
                                          strPrimeCareFile = p.PrimeCareFile,
                                          strPrimeCareFileName = p.PrimeCareFileName,
                                          strPrimeCareFileDescription = p.PrimeCareFileDescription,
                                          dteCreatedDate = Comman.getClientTime(p.CreatedDate.ToString()),
                                          strCreatedByUser = u.FirstName + u.LastName,
                                      }).ToList();
                return primeCareFiles;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SavePrimeCareFile(ref string log, TblPrimeCare file)
        {
            try
            {
                var AddedFile = db.TblPrimeCare.Add(file);
                db.SaveChanges();
                if (AddedFile.Entity.PrimeCareId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Prime Care File", AddedFile.Entity.PrimeCareId);
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

        public TblPrimeCare GetPrimeCareFileById(ref string log, int? id)
        {
            try
            {
                var primecareFile = db.TblPrimeCare.Where(x => x.PrimeCareId == id && x.IsActive == true).FirstOrDefault();
                return primecareFile;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditPrimeCareFile(ref string log, TblPrimeCare PCFile)
        {
            try
            {
                var oldFile = db.TblPrimeCare.Where(x => x.PrimeCareId == PCFile.PrimeCareId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldFile.PrimeCareId = PCFile.PrimeCareId;
                oldFile.PrimeCareFileName = PCFile.PrimeCareFileName;
                if (PCFile.PrimeCareFile != null && PCFile.PrimeCareFile != "")
                {
                    oldFile.PrimeCareFile = PCFile.PrimeCareFile;
                }
                oldFile.PrimeCareFileDescription = PCFile.PrimeCareFileDescription;
                oldFile.UpdatedDate = PCFile.UpdatedDate;
                oldFile.UpdatedBy = PCFile.UpdatedBy;
                db.TblPrimeCare.Update(oldFile);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Prime Care File", oldFile.PrimeCareId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeletePrimeCareFile(ref string log, int id, bool delete)
        {
            try
            {
                if (delete)
                {
                    var primeCareFile = db.TblPrimeCare.Where(x => x.IsActive == true && x.PrimeCareId == id).FirstOrDefault();
                    if (primeCareFile != null)
                    {
                        primeCareFile.IsArchived = true;
                        primeCareFile.IsActive = false;
                        db.TblPrimeCare.Update(primeCareFile);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Prime Care File", primeCareFile.PrimeCareId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var primeCareFile = db.TblPrimeCare.Where(x => x.IsActive == true && x.PrimeCareId == id).FirstOrDefault();
                    if (primeCareFile != null)
                    {
                        primeCareFile.IsArchived = false;
                        primeCareFile.IsActive = true;
                        db.TblPrimeCare.Update(primeCareFile);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Prime Care File", primeCareFile.PrimeCareId);
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

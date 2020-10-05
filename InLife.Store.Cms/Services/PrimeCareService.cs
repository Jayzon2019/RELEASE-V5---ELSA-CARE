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
    public class PrimeCareService
    {
        PrimeCareRepo PCR = new PrimeCareRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<PrimeCareViewModel> GetPrimeCareFiles(ref string log)
        {
            try
            {
                var primeCareFiles = PCR.GetPrimeCareFiles(ref log);
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

        public string SavePrimeCareFile(ref string log, PrimeCareViewModel primeCareVM)
        {
            try
            {
                var uploadPathWithfileName = "";
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                var file = files[0];
                if (file != null && file.Length > 0)
                {
                    string uploadPath = "PrimeCareFiles";
                    uploadPathWithfileName = Comman.SaveFileToDirectory(file, uploadPath);
                }
                TblPrimeCare PCFile = new TblPrimeCare
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    PrimeCareId = primeCareVM.intPrimeCareId,
                    PrimeCareFile = primeCareVM.strPrimeCareFile,
                    PrimeCareFileName = primeCareVM.strPrimeCareFileName,
                    PrimeCareFileDescription = primeCareVM.strPrimeCareFileDescription,
                };
                PCFile.PrimeCareFile = uploadPathWithfileName;
                PCR.SavePrimeCareFile(ref log, PCFile);
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

        public PrimeCareViewModel GetPrimeCareFileById(ref string log, int? id)
        {
            try
            {
                var PCFile = PCR.GetPrimeCareFileById(ref log, id);

                PrimeCareViewModel PCVM = new PrimeCareViewModel
                {
                    blnIsActive = PCFile.IsActive,
                    intCreatedBy = PCFile.CreatedBy,
                    intPrimeCareId = PCFile.PrimeCareId,
                    strPrimeCareFile = PCFile.PrimeCareFile,
                    strPrimeCareFileName = PCFile.PrimeCareFileName,
                    strPrimeCareFileDescription = PCFile.PrimeCareFileDescription
                };
                if (PCFile.CreatedDate != null)
                {
                    PCVM.dteCreatedDate = Comman.getClientTime(PCFile.CreatedDate.ToString());
                }
                if (PCFile.UpdatedDate != null)
                {
                    PCVM.dteUpdatedDate = Comman.getClientTime(PCFile.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(PCVM.intCreatedBy));
                PCVM.strCreatedByUser = createdBy;
                if (PCVM.intUpdatedBy > 0)
                {
                    if (PCVM.intCreatedBy != PCVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(PCVM.intUpdatedBy);
                        PCVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        PCVM.strUpdatedByUser = createdBy;
                    }
                }

                return PCVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditPrimeCareFile(ref string log, PrimeCareViewModel PCFile)
        {
            try
            {
                var uploadPathWithfileName = "";
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                if (files.Count() > 0) {
                    var file = files[0];
                    if (file != null && file.Length > 0)
                    {
                        //string uploadPath = "PrimeCareFiles";
                       // uploadPathWithfileName = Comman.SaveFileToDirectory(file, uploadPath);
                        uploadPathWithfileName = Comman.ConvertImageToBase64String(file);
                    }
                }
                TblPrimeCare PCF = new TblPrimeCare
                {
                    PrimeCareId = PCFile.intPrimeCareId,
                    PrimeCareFile = PCFile.strPrimeCareFile,
                    PrimeCareFileName = PCFile.strPrimeCareFileName,
                    PrimeCareFileDescription = PCFile.strPrimeCareFileDescription,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value)
                };
                PCF.PrimeCareFile = uploadPathWithfileName;
                PCR.EditPrimeCareFile(ref log, PCF);

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
                PCR.Deactivate_DeletePrimeCareFile(ref log, id, delete);
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

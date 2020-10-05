using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Repos;
using InLifeCMS.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Services
{
    public class ProductsService
    {
        ProductsRepo PR = new ProductsRepo();
        LogsRepo lR = new LogsRepo();
        UsersRepo UR = new UsersRepo();

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public List<ProductsViewModel> GetProductsList(ref string log)
        {
            try
            {
                List<ProductsViewModel> PVMList = new List<ProductsViewModel>();
                var proList = PR.GetProductList(ref log);
                foreach (var pro in proList)
                {
                    ProductsViewModel PVM = new ProductsViewModel
                    {
                        intProductId = pro.ProductId,
                        strProductName = pro.ProductName,
                        strProductImg = pro.ProductImg,
                        strProductCode = pro.ProductCode,
                        strProductPrice = pro.ProductPrice,
                        intSortNum = pro.SortNum,
                    };
                    PVMList.Add(PVM);
                }
                return PVMList;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }

        }

        public string SaveProduct(ref string log, ProductsViewModel ProVM)
        {
            try
            {
                var uploadPathWithfileName = "";
                var files = httpContextAccessor.HttpContext.Request.Form.Files;
                var file = files[0];
                if (file != null && file.Length > 0)
                {
                    uploadPathWithfileName = Comman.ConvertImageToBase64String(file);
                }
                TblProducts Pro = new TblProducts
                {
                    CreatedDate = DateTime.Now,
                    CreatedBy = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Sid)?.Value),
                    IsActive = true,
                    ProductId = ProVM.intProductId,
                    ProductImg = ProVM.strProductImg,
                     ProductName = ProVM.strProductName,
                     ProductPrice = ProVM.strProductPrice,
                    ProductCode = ProVM.strProductCode,
                    ShortDescription = ProVM.strShortDescription,
                    PriceWithOffer = ProVM.strPriceWithOffer,
                    SortNum = ProVM.intSortNum,
                };
                Pro.ProductImg = uploadPathWithfileName;
                PR.SaveProduct(ref log, Pro);
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

        public ProductsViewModel GetProductById(ref string log, int? id)
        {
            try
            {
                var pro = PR.GetProductById(ref log, id);

                ProductsViewModel PVM = new ProductsViewModel
                {
                    blnIsActive = pro.IsActive,
                    intCreatedBy = pro.CreatedBy,
                    intProductId = pro.ProductId,
                    intUpdatedBy = pro.UpdatedBy,
                    strProductImg = pro.ProductImg,
                    strProductName = pro.ProductName,
                     strProductPrice  = pro.ProductPrice,
                    strProductCode = pro.ProductCode,
                    strShortDescription = pro.ShortDescription,
                    intSortNum = pro.SortNum,
                    strPriceWithOffer = pro.PriceWithOffer
                };
                if (pro.CreatedDate != null)
                {
                    PVM.dteCreatedDate = Comman.getClientTime(pro.CreatedDate.ToString());
                }
                if (pro.UpdatedDate != null)
                {
                    PVM.dteUpdatedDate = Comman.getClientTime(pro.UpdatedDate.ToString());
                }
                var createdBy = UR.GETUserCreatedBy_UpdatedBy(ref log, Convert.ToInt32(PVM.intCreatedBy));
                PVM.strCreatedByUser = createdBy;
                if (PVM.intUpdatedBy > 0)
                {
                    if (PVM.intCreatedBy != PVM.intUpdatedBy)
                    {
                        int uId = Convert.ToInt32(PVM.intUpdatedBy);
                        PVM.strUpdatedByUser = UR.GETUserCreatedBy_UpdatedBy(ref log, uId);
                    }
                    else
                    {
                        PVM.strUpdatedByUser = createdBy;
                    }
                }

                return PVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditProducts(ref string log, ProductsViewModel pro)
        {
            try
            {
                TblProducts p = new TblProducts
                {
                    ProductId = pro.intProductId,
                    ProductImg = pro.strProductImg,
                    ProductName = pro.strProductName,
                    ProductPrice = pro.strProductPrice,
                    ProductCode = pro.strProductCode,
                    ShortDescription = pro.strShortDescription,
                    PriceWithOffer = pro.strPriceWithOffer,
                    SortNum = pro.intSortNum,
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
                        p.ProductImg = uploadPathWithfileName;
                    }
                }
                PR.EditProduct(ref log, p);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public void Deactivate_DeleteProduct(ref string log, int id, bool delete)
        {
            try
            {
                PR.Deactivate_DeleteProduct(ref log, id, delete);
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

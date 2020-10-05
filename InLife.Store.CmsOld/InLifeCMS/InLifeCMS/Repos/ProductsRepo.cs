using InLifeCMS.Helpers;
using InLifeCMS.Models;
using InLifeCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.Repos
{
    public class ProductsRepo
    {
        //dbinlifecmshostContext db = new dbinlifecmshostContext();
        InLifePrimeCareStoreContext db = new InLifePrimeCareStoreContext();
        LogsRepo lR = new LogsRepo();
        LogsService LS = new LogsService();

        public List<TblProducts> GetProductList(ref string log)
        {
            try
            {
                var productsList = db.TblProducts.Where(x => x.IsActive == true).OrderBy(x=>x.SortNum).ToList();

                return productsList;

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SaveProduct(ref string log, TblProducts pro)
        {
            try
            {
                var AddedProduct = db.TblProducts.Add(pro);
                db.SaveChanges();
                if (AddedProduct.Entity.ProductId > 0)
                {
                    var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Added.ToString(), "a new Product", AddedProduct.Entity.ProductId);
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

        public TblProducts GetProductById(ref string log, int? id)
        {
            try
            {
                var pro = db.TblProducts.Where(x => x.ProductId == id && x.IsActive == true).FirstOrDefault();
                return pro;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void EditProduct(ref string log, TblProducts pro)
        {
            try
            {
                var oldPro = db.TblProducts.Where(x => x.ProductId == pro.ProductId && x.IsArchived == false && x.IsActive == true).FirstOrDefault();
                oldPro.ProductId = pro.ProductId;
                if (pro.ProductCode != null)
                {
                    oldPro.ProductCode = pro.ProductCode;
                }
                else
                {
                    oldPro.ProductCode = "";
                }
                if (pro.ProductImg != null &&   pro.ProductImg != "")
                {
                oldPro.ProductImg = pro.ProductImg;
                }
                oldPro.ProductName = pro.ProductName;
                oldPro.ShortDescription = pro.ShortDescription;
                oldPro.PriceWithOffer = pro.PriceWithOffer;
                oldPro.ProductPrice = pro.ProductPrice;
                oldPro.SortNum = pro.SortNum;
                oldPro.UpdatedBy = pro.UpdatedBy;
                oldPro.UpdatedDate = pro.UpdatedDate;
                db.TblProducts.Update(oldPro);
                db.SaveChanges();
                var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Updated.ToString(), "Product Entery", oldPro.ProductId);
                LS.SaveActivityLogs(Comman.ActivityActions.Updated.ToString(), activityLog);
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
                if (delete)
                {
                    var pro = db.TblProducts.Where(x => x.IsActive == true && x.ProductId == id).FirstOrDefault();
                    if (pro != null)
                    {
                        pro.IsArchived = true;
                        pro.IsActive = false;
                        db.TblProducts.Update(pro);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deleted.ToString(), "Product Entery", pro.ProductId);
                        LS.SaveActivityLogs(Comman.ActivityActions.Deleted.ToString(), activityLog);
                    }
                }
                else
                {
                    var pro = db.TblProducts.Where(x => x.IsActive == true && x.ProductId == id).FirstOrDefault();
                    if (pro != null)
                    {
                        pro.IsArchived = false;
                        pro.IsActive = true;
                        db.TblProducts.Update(pro);
                        db.SaveChanges();
                        var activityLog = Comman.ActivityAddlogDescription(Comman.ActivityActions.Deactivated.ToString(), "Product Entery", pro.ProductId);
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

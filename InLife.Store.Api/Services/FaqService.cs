using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using InLife.Store.Api.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Services
{
    public class FaqService
    {
        FaqRepo FR = new FaqRepo();
        LogsRepo lR = new LogsRepo();

        public List<TblFaqCategories> GetFaqCatList(ref string log)
        {
            try
            {
                return FR.GetFaqCatList(ref log);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

            public List<TblFaq> GetFaqListByCatId(int id , ref string log)
            {
                try
                {
                    return FR.GetFaqListByCatId(id , ref log);

                }
                catch (Exception ex)
                {
                    string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                    var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                    lR.SaveExceptionLogs(exLog, ex, methodName);
                    return null;
                }
            }
        public List<TblFaq> GetFaqList(ref string log)
            {
                try
                {
                    return FR.GetFaqList(ref log);

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

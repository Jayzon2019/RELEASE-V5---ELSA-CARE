using InLife.Store.Api.Helpers;
using InLife.Store.Api.Models;
using InLife.Store.Api.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InLife.Store.Api.Services
{
    public class HomeService
    {
        HomeRepo HR = new HomeRepo();
        LogsRepo lR = new LogsRepo();

        public List<TblHero> GetHeroSliders(ref string log)
        {
            try
            {
                return HR.GetHeroSliders(ref log);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public List<TblProducts> GetProductsList(ref string log)
        {
            try
            {
                return HR.GetProductsList(ref log);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }
        public List<TblProductDetails> GetProductsDetailList(ref string log)
        {
            try
            {
                return HR.GetProductsDetailList(ref log);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public TblKeyMetrics GetPageId_SetPageView(ref string log , string page)
        {
            try
            {
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                TblKeyMetrics pageView = new TblKeyMetrics
                {
                    Ip = myIP,
                    MachineName = hostName,
                    PageName = page,
                    PageViewedAt = DateTime.Now,
                    PageViews = 1,  
                };
                
                return HR.GetPageId_SetPageView(ref log , pageView);
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public void SetUserLeaveTime(ref string log , int id)
        {
            try
            {
                var oldpage = HR.GetPageViewById(ref log , id);
                if(oldpage != null)
                {
                    oldpage.PageLeftAt = DateTime.Now;
                    var hours = (DateTime.Now - oldpage.PageViewedAt).Value.TotalHours.ToString();
                    var mins = (DateTime.Now - oldpage.PageViewedAt).Value.Minutes.ToString();
                    oldpage.Sessions = hours + " : " + mins;
                }
                HR.SetUserLeaveTime(ref log , oldpage);

            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
            }
        }

        public List<TblPrimeCare> GetFiles(ref string log)
        {
            try
            {
                return HR.GetFiles(ref log);
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
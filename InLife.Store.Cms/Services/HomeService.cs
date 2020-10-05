using InLifeCMS.Helpers;
using InLifeCMS.Repos;
using InLifeCMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InLife.Store.Core.Models;

namespace InLifeCMS.Services
{
    public class HomeService
    {
        LogsRepo lR = new LogsRepo();
        HomeRepo HR = new HomeRepo();
        public KeyMetricsViewModel GetHomeData(ref string log)
        {
            try
            {
                var keyMetrics = HR.GetKeyMetrics(ref log);
                var totalHours = 0;
                var totalMins = 0;
                foreach (var item in keyMetrics)
                {
                    if (item.PageLeftAt != null && item.PageViewedAt != null)
                    {
                        var s = item.Sessions.Split(":");
                        var h = s[0].Split(".");
                        if (totalHours == 0)
                        {
                            totalHours = Convert.ToInt32(h[0]);
                        }
                        else
                        {
                            totalHours += Convert.ToInt32(h[0]);
                        }
                        if (totalMins == 0)
                        {
                            totalMins = Convert.ToInt32(s[1]);
                        }
                        else
                        {
                            totalMins += Convert.ToInt32(s[1]);
                        }
                    }
                }

                var pageViews = keyMetrics.Sum(x => x.PageViews);
                    KeyMetricsViewModel keyVM = new KeyMetricsViewModel
                    {
                        strSessions = totalHours + " : " + totalMins,
                        intPageViews = pageViews,
                    };
                keyVM.intUsers = HR.GetUserCount(ref log);
                keyVM.lstActivityLogs = GetActivityLogs(ref log);
                foreach (var item in keyVM.lstActivityLogs)
                {
                    item.dteActivityDate = Comman.getClientTime(item.dteActivityDate.ToString());
                }
                return keyVM;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                var exLog = Comman.ExceptionLogBulder(log, methodName, ex);
                lR.SaveExceptionLogs(exLog, ex, methodName);
                return null;
            }
        }

        public List<ActivityLogsViewModel> GetActivityLogs(ref string log)
        {
            try
            {
                var activityLogs = HR.GetActivityLogs(ref log);
                List<ActivityLogsViewModel> lstactivityLogs = new List<ActivityLogsViewModel>();
                foreach (var actLog in activityLogs)
                {
                    ActivityLogsViewModel logs = new ActivityLogsViewModel
                    {
                        strActionPerfomed = actLog.ActionPerfomed,
                        strActivityDescription = actLog.ActivityDescription,
                        strActivityBy = actLog.ActivityBy,
                        dteActivityDate = Comman.getClientTime(actLog.ActivityDate.ToString()),
                        intActivityById = actLog.ActivityById,
                        strIpAddress = actLog.IpAddress,
                    };
                    lstactivityLogs.Add(logs);
                };
                return lstactivityLogs;
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

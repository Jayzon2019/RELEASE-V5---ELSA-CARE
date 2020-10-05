using InLifeCMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLifeCMS.ViewModel
{
    public class DashboardDataViewModel
    {
        public List<ActivityLogsViewModel> lstActivityLogs { get; set; }
        public KeyMetricsViewModel lstkeyMetricsVM { get; set; }
    }
}

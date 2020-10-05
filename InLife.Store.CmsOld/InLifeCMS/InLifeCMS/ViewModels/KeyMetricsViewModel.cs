using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class KeyMetricsViewModel
    {
        [Key]
        public int intKeyMetricsId { get; set; }
        public string strPageName { get; set; }
        public int? intPageViews { get; set; }
        public string strSessions { get; set; }
        public int intUsers { get; set; }

        public List<ActivityLogsViewModel> lstActivityLogs { get; set; }
    }
}

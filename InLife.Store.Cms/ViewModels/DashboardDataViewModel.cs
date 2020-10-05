using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
    public class DashboardDataViewModel
    {
        public List<ActivityLogViewModel> ActivityLogs { get; set; }

        public KeyMetricsViewModel KeyMetrics { get; set; }
    }
}

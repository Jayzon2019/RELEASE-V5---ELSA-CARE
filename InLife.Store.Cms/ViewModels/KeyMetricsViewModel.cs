using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
    public partial class KeyMetricsViewModel
    {
        [Key]
        public int Id { get; set; }

        public string PageName { get; set; }

        public int? PageViews { get; set; }

        public string Sessions { get; set; }

        public int Users { get; set; }

        public List<ActivityLogsViewModel> ActivityLogs { get; set; }
    }
}

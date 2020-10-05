using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblKeyMetrics
    {
        public int KeyMetricsId { get; set; }
        public string PageName { get; set; }
        public int? PageViews { get; set; }
        public string Sessions { get; set; }
        public DateTime? PageViewedAt { get; set; }
        public DateTime? PageLeftAt { get; set; }
        public string MachineName { get; set; }
        public string Ip { get; set; }
    }
}

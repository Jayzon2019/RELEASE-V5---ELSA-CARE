using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblActivityLogs
    {
        public int ActivityLogId { get; set; }
        public string ActivityBy { get; set; }
        public string ActionPerfomed { get; set; }
        public string ActivityDescription { get; set; }
        public string IpAddress { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int? ActivityById { get; set; }
    }
}

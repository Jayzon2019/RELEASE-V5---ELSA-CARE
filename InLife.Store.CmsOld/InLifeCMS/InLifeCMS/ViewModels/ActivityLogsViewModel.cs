using System;
using System.Collections.Generic;

namespace InLifeCMS.ViewModel
{
    public partial class ActivityLogsViewModel
    {
        public int intActivityLogId { get; set; }
        public string strActivityBy { get; set; }
        public string strActionPerfomed { get; set; }
        public string strActivityDescription { get; set; }
        public string strIpAddress { get; set; }
        public DateTime? dteActivityDate { get; set; }
        public int? intActivityById { get; set; }
    }
}

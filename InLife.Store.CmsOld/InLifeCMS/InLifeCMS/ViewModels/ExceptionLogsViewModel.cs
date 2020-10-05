using System;
using System.Collections.Generic;

namespace InLifeCMS.ViewModel
{
    public partial class ExceptionLogsViewModel
    {
        public int intExId { get; set; }
        public string strExMsg { get; set; }
        public string strExSource { get; set; }
        public string strExUrl { get; set; }
        public DateTime dteExDate { get; set; }
    }
}

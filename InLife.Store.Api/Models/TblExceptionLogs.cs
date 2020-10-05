using System;
using System.Collections.Generic;

namespace InLife.Store.Api.Models
{
    public partial class TblExceptionLogs
    {
        public int ExId { get; set; }
        public string ExMsg { get; set; }
        public string ExSource { get; set; }
        public string ExUrl { get; set; }
        public DateTime ExDate { get; set; }
        public string ExInner { get; set; }
    }
}

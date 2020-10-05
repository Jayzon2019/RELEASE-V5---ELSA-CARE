using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
    public class ExceptionLog : Entity<int>
    {
        public string ExMsg { get; set; }
        public string ExSource { get; set; }
        public string ExUrl { get; set; }
        public DateTimeOffset ExDate { get; set; }
        public string ExInner { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
    public partial class ExceptionLogViewModel
    {
        public int ExId { get; set; }
        public string ExMsg { get; set; }
        public string ExSource { get; set; }
        public string ExUrl { get; set; }
        public DateTime ExDate { get; set; }
    }
}

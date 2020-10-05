using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InLife.Store.Cms.ViewModels
{
    public partial class EmailCredentialsViewModel
    {
        [Key]
        public int intEmailCredentialsId { get; set; }
        public string strUserName { get; set; }
        public string strPassword { get; set; }
        public string strSmtp { get; set; }
        public int? intPort { get; set; }
        public bool? blnIsBodyHtml { get; set; }
        public bool? blnEnableSsl { get; set; }
    }
}

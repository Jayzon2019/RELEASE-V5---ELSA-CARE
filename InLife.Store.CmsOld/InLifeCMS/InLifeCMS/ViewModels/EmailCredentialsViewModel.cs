using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
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

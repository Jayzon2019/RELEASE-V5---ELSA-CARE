using System;
using System.Collections.Generic;

namespace InLifeCMS.Models
{
    public partial class TblEmailCredentials
    {
        public int EmailCredentialsId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public int? Port { get; set; }
        public bool? IsBodyHtml { get; set; }
        public bool? EnableSsl { get; set; }
    }
}

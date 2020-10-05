using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InLifeCMS.ViewModel
{
    public partial class PrimeCareViewModel
    {
        [Key]
        public int intPrimeCareId { get; set; }
        public string strPrimeCareFile { get; set; }
        public string strPrimeCareFileName { get; set; }
        public string strPrimeCareFileDescription { get; set; }
        public string strCreatedByUser { get; set; }
        public string strUpdatedByUser { get; set; }
        public DateTime dteCreatedDate { get; set; }
        public int intCreatedBy { get; set; }
        public DateTime? dteUpdatedDate { get; set; }
        public int? intUpdatedBy { get; set; }
        public bool blnIsActive { get; set; }
        public bool blnIsArchived { get; set; }
    }
}
